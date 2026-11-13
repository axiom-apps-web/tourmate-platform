using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Tourmate.Platform.TourMonitoring.Application.CommandServices;
using Tourmate.Platform.TourMonitoring.Application.QueryServices;
using Tourmate.Platform.TourMonitoring.Domain.Model.Queries;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Transform;

namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available ActiveTour endpoints")]
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

}