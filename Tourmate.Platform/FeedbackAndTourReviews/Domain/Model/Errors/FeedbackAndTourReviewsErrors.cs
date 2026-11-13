using Tourmate.Platform.Shared.Domain.Model.Errors;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Errors;

public class FeedbackAndTourReviewsErrors
{
    public static readonly Error ReviewNotFound =
        Error.NotFound("FeedbackAndTourReviews.ReviewNotFound", "The specified review was not found.");

    public static readonly Error CommentNotFound =
        Error.NotFound("FeedbackAndTourReviews.CommentNotFound", "The specified comment was not found in this review.");

    public static readonly Error InvalidRating =
        Error.Validation("FeedbackAndTourReviews.InvalidRating", "The rating must be between 1 and 5.");

    public static readonly Error InvalidCommentContent =
        Error.Validation("FeedbackAndTourReviews.InvalidCommentContent", "The comment content cannot be empty.");
}