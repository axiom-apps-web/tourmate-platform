namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

public record CommentResource(
    int Id,
    int ReviewId,
    string Content,
    DateTimeOffset? CreatedAt
);