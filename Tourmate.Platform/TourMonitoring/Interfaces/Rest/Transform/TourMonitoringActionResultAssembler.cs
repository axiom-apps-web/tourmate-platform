using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Tourmate.Platform.TourMonitoring.Domain.Model;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;

namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Transform;

public static class TourMonitoringActionResultAssembler
{
     // --- Helper for transforming TourMonitoringError to StatusCode ---
    private static int ToStatusCodeFromTourMonitoringError(TourMonitoringError error)
    {
        return error switch
        {
          
            TourMonitoringError.ActiveTourNotFound => StatusCodes.Status404NotFound,
           
            TourMonitoringError.DatabaseError => StatusCodes.Status500InternalServerError,
            TourMonitoringError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest // Default
        };
    }

    // --- Specific Assembler Methods ---

  
    

    public static IActionResult ToActionResultFromCreateActiveTourResult(
        ControllerBase controller,
        Result<ActiveTour> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ActiveTour, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromTourMonitoringError((TourMonitoringError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    

    public static IActionResult ToActionResultFromGetActiveTourByIdResult(
        ControllerBase controller,
        ActiveTour? activeTour, // Direct activeTour object, not a Result
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<ActiveTour, IActionResult> successAction)
    {
        if (activeTour is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromTourMonitoringError(TourMonitoringError.ActiveTourNotFound),
                TourMonitoringError.ActiveTourNotFound,
                errorLocalizer[nameof(TourMonitoringError.ActiveTourNotFound)]
            );
        return successAction(activeTour);
    }

    public static IActionResult ToActionResultFromGetAllActiveToursByCategoryIdResult(
        ControllerBase controller,
        IEnumerable<ActiveTour> activeTours, // Direct collection, not a Result
        IStringLocalizer<ErrorMessages> errorLocalizer, // Still need localizer for potential future errors
        ProblemDetailsFactory problemDetailsFactory, // Still need factory for potential future errors
        Func<IEnumerable<ActiveTour>, IActionResult> successAction)
    {
        // Assuming an empty collection is a valid success, not an error.
        // If a specific error should be returned for an empty collection,
        // that logic would need to be added here or in the query service.
        return successAction(activeTours);
    }
}