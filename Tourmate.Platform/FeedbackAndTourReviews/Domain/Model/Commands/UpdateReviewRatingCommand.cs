using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;

public record UpdateReviewRatingCommand(ReviewId ReviewId, int Rating)
{
    public UpdateReviewRatingCommand(int reviewId, int rating) : this(new ReviewId(reviewId), rating)
    {
    }
}