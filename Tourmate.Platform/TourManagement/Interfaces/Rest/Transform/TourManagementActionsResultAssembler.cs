using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Tourmate.Platform.TourManagement.Domain.Model;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;

namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Transform;

public static class TourManagementActionsResultAssembler
{
    private static int ToStatusCodeFromTourManagementError(ETourManagementError error)
    {
        return error switch
        {
            ETourManagementError.TourNotFound => StatusCodes.Status404NotFound,
            ETourManagementError.TourScheduleNotFound => StatusCodes.Status404NotFound,
            ETourManagementError.DatabaseError => StatusCodes.Status500InternalServerError,
            ETourManagementError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateTourResult(
        ControllerBase controller,
        Result<Tour> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Tour, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromTourManagementError((ETourManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetTourByIdResult(
        ControllerBase controller,
        Tour? tour,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Tour, IActionResult> successAction)
    {
        if (tour is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromTourManagementError(ETourManagementError.TourNotFound),
                ETourManagementError.TourNotFound,
                errorLocalizer[nameof(ETourManagementError.TourNotFound)]);
        }
        return successAction(tour);
    }
    
    public static IActionResult ToActionResultFromUpdateTourResult(
        ControllerBase controller,
        Result<Tour> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Tour, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromTourManagementError((ETourManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    
    public static IActionResult ToActionResultFromDeleteTourResult(
        ControllerBase controller,
        Result result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess) return controller.NoContent();

        var statusCode = ToStatusCodeFromTourManagementError((ETourManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}