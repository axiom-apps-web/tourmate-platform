using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Queries;

public record GetReviewByIdQuery(ReviewId ReviewId)
{
    public GetReviewByIdQuery(int reviewId) : this(new ReviewId(reviewId))
    {
    }
}