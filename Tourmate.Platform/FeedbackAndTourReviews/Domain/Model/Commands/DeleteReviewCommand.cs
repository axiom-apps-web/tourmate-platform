using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;

public record DeleteReviewCommand(ReviewId ReviewId)
{
    public DeleteReviewCommand(int reviewId) : this(new ReviewId(reviewId))
    {
    }
}