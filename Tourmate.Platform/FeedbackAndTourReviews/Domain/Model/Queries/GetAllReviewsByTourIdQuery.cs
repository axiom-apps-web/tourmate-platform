using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Queries;

public record GetAllReviewsByTourIdQuery(TourId TourId)
{
    public GetAllReviewsByTourIdQuery(int tourId) : this(new TourId(tourId))
    {
    }
}