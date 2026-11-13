using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;

public record CreateReviewCommand(UserId UserId, TourId TourId, int Rating, string? Comment)
{
    public CreateReviewCommand(int userId, int tourId, int rating, string? comment)
        : this(new UserId(userId), new TourId(tourId), rating, comment)
    {
    }
}