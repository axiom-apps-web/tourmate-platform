using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Tourmate.Platform.TourMonitoring.Application.CommandServices;
using Tourmate.Platform.TourMonitoring.Application.QueryServices;
using Tourmate.Platform.TourMonitoring.Domain.Model.Commands;
using Tourmate.Platform.TourMonitoring.Domain.Model.Queries;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Transform;

namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Active Tour endpoints")]
public class ActiveToursController(
    IActiveTourQueryService activeTourQueryService,
    IActiveTourCommandService activeTourCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer, // Renamed for clarity
    ProblemDetailsFactory problemDetailsFactory) // Inject ProblemDetailsFactory
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{activeTourId:int}")]
    [EndpointSummary("Get a active tour by its id")]
    [EndpointDescription("Get a active tour by its id")]
    [ProducesResponseType(typeof(ActiveTourResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveTourById([FromRoute] int activeTourId, CancellationToken cancellationToken)
    {
        var getActiveTourByIdQuery = new GetActiveTourByIdQuery(activeTourId);
        var activeTour = await activeTourQueryService.Handle(getActiveTourByIdQuery, cancellationToken);

        return TourMonitoringActionResultAssembler.ToActionResultFromGetActiveTourByIdResult(
            this,
            activeTour,
            _errorLocalizer,
            _problemDetailsFactory,
            foundActiveTour => Ok(ActiveTourResourceFromEntityAssembler.ToResourceFromEntity(foundActiveTour))
        );
    }

    [HttpGet]
    [EndpointSummary("Get all active tours")]
    [EndpointDescription("Get all active tours")]
    [ProducesResponseType(typeof(IEnumerable<ActiveTourResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllActiveTours(CancellationToken cancellationToken)
    {
        var getAllActiveToursQuery = new GetAllActiveToursQuery();
        var activeTours = await activeTourQueryService.Handle(getAllActiveToursQuery, cancellationToken);
        var activeTourResources = activeTours.Select(ActiveTourResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(activeTourResources);
    }

    [HttpPost]
    [EndpointSummary("Create a active tour")]
    [EndpointDescription("Create a active tour")]
    [ProducesResponseType(typeof(ActiveTourResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateActiveTour([FromBody] CreateActiveTourResource resource,
        CancellationToken cancellationToken)
    {
        var createActiveTourCommand = CreateActiveTourCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await activeTourCommandService.Handle(createActiveTourCommand, cancellationToken);

        return TourMonitoringActionResultAssembler.ToActionResultFromCreateActiveTourResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdActiveTour => CreatedAtAction(nameof(GetActiveTourById), new { activeTourId = createdActiveTour.Id },
                ActiveTourResourceFromEntityAssembler.ToResourceFromEntity(createdActiveTour))
        );
    }

    [HttpPut("{activeTourId:int}")]
    [EndpointSummary("Update an active tour")]
    [EndpointDescription("Update an active tour")]
    [ProducesResponseType(typeof(ActiveTourResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateActiveTour([FromRoute] int activeTourId,
        [FromBody] UpdateActiveTourResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateActiveTourCommandFromResourceAssembler.ToCommandFromResource(activeTourId, resource);
        var result = await activeTourCommandService.Handle(command, cancellationToken);

        return TourMonitoringActionResultAssembler.ToActionResultFromUpdateActiveTourResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updatedActiveTour => Ok(ActiveTourResourceFromEntityAssembler.ToResourceFromEntity(updatedActiveTour)));
    }

    [HttpDelete("{activeTourId:int}")]
    [EndpointSummary("Delete an active tour")]
    [EndpointDescription("Delete an active tour")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActiveTour([FromRoute] int activeTourId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteActiveTourCommand(activeTourId);
        var result = await activeTourCommandService.Handle(command, cancellationToken);

        return TourMonitoringActionResultAssembler.ToActionResultFromDeleteActiveTourResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory);
    }
}