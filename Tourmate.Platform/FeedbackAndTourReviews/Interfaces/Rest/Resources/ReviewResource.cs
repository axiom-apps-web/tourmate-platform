namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

public record ReviewResource(
    int Id,
    int UserId,
    int TourId,
    int Rating,
    DateTimeOffset? CreatedAt,
    IEnumerable<CommentResource> Comments
);