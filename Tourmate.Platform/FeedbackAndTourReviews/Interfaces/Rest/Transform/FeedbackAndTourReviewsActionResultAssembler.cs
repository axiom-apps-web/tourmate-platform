using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;

public static class FeedbackAndTourReviewsActionResultAssembler
{
    // --- Helper for transforming FeedbackAndTourReviewsError to StatusCode ---
    private static int ToStatusCodeFromFeedbackAndTourReviewsError(FeedbackAndTourReviewsError error)
    {
        return error switch
        {
            FeedbackAndTourReviewsError.ReviewNotFound => StatusCodes.Status404NotFound,
            FeedbackAndTourReviewsError.CommentNotFound => StatusCodes.Status404NotFound,
            FeedbackAndTourReviewsError.InvalidRating => StatusCodes.Status400BadRequest,
            FeedbackAndTourReviewsError.InvalidCommentContent => StatusCodes.Status400BadRequest,
            FeedbackAndTourReviewsError.DatabaseError => StatusCodes.Status500InternalServerError,
            FeedbackAndTourReviewsError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest // Default
        };
    }

    // --- Generic method for any command result (create, update, delete) ---
    public static IActionResult ToActionResultFromCommandResult<T>(
        ControllerBase controller,
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromFeedbackAndTourReviewsError((FeedbackAndTourReviewsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetReviewByIdResult(
        ControllerBase controller,
        Review? review, // Direct review object, not a Result
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Review, IActionResult> successAction)
    {
        if (review is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromFeedbackAndTourReviewsError(FeedbackAndTourReviewsError.ReviewNotFound),
                FeedbackAndTourReviewsError.ReviewNotFound,
                errorLocalizer[nameof(FeedbackAndTourReviewsError.ReviewNotFound)]
            );
        return successAction(review);
    }
}