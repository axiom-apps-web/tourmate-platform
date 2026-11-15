using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Tourmate.Platform.TourManagement.Application.CommandService;
using Tourmate.Platform.TourManagement.Application.QueryService;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Model.Queries;
using Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;
using Tourmate.Platform.TourManagement.Interfaces.Rest.Transform;

namespace Tourmate.Platform.TourManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Tour endpoints")]
public class TourController(
    ITourCommandService tourCommandService,
    ITourQueryService tourQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet("{tourId:int}")]
    [EndpointSummary("Get a Tour by Id")]
    [EndpointDescription("Get a Tour by Id its unique identifier")]
    [ProducesResponseType(typeof(TourResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTourById(int tourId, CancellationToken cancellationToken)
    {
        var getTourByIdQuery = new GetTourByIdQuery(tourId);
        var tour = await tourQueryService.Handle(getTourByIdQuery, cancellationToken);
        
        return TourManagementActionsResultAssembler.ToActionResultFromGetTourByIdResult(
            this, tour, _errorLocalizer, _problemDetailsFactory,
            foundTour => Ok(TourResourceFromEntityAssembler.ToResourceFromEntity(foundTour)));
    }

    [HttpGet]
    [EndpointSummary("Get all Tours")]
    [EndpointDescription("Get all Tours")]
    [ProducesResponseType(typeof(List<TourResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTours(CancellationToken cancellationToken)
    {
        var getAllToursQuery = new GetAllToursQuery();
        var tours = await tourQueryService.Handle(getAllToursQuery, cancellationToken);
        var tourResources = tours.Select(TourResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tourResources);
    }

    [HttpPost]
    [EndpointSummary("Create Tour")]
    [EndpointDescription("Create a new Tour")]
    [ProducesResponseType(typeof(TourResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTour(CreateTourResource resource, CancellationToken cancellationToken)
    {
        var createTourCommand = CreateTourCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await tourCommandService.Handle(createTourCommand, cancellationToken);

        return TourManagementActionsResultAssembler.ToActionResultFromCreateTourResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdTour => CreatedAtAction(nameof(GetTourById), new { tourId = createdTour.Id },
                TourResourceFromEntityAssembler.ToResourceFromEntity(createdTour)));
    }
    
    [HttpPut("{tourId:int}")]
    [EndpointSummary("Update a tour")]
    [EndpointDescription("Update a tour")]
    [ProducesResponseType(typeof(TourResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTour(int tourId,
        UpdateTourResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateTourCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await tourCommandService.Handle(tourId, command, cancellationToken);

        return TourManagementActionsResultAssembler.ToActionResultFromUpdateTourResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updatedActiveTour => Ok(TourResourceFromEntityAssembler.ToResourceFromEntity(updatedActiveTour)));
    }
    
    [HttpDelete("{tourId:int}")]
    [EndpointSummary("Delete an active tour")]
    [EndpointDescription("Delete an active tour")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActiveTour(int tourId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTourCommand(tourId);
        var result = await tourCommandService.Handle(command, cancellationToken);

        return TourManagementActionsResultAssembler.ToActionResultFromDeleteTourResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory);
    }
    
}