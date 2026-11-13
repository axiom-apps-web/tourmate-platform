namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

public record CreateReviewResource(
    int UserId,
    int TourId,
    int Rating,
    string? Comment);