using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;

public record AddCommentToReviewCommand(ReviewId ReviewId, string Content)
{
    public AddCommentToReviewCommand(int reviewId, string content) : this(new ReviewId(reviewId), content)
    {
    }
}