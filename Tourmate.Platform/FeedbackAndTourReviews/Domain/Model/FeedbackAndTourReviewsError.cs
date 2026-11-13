namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model;

public enum FeedbackAndTourReviewsError
{
    None,
    ReviewNotFound,
    CommentNotFound,
    InvalidRating,
    InvalidCommentContent,
    DatabaseError,
    InternalServerError,
}