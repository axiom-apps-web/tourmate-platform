using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;

public record UpdateCommentCommand(ReviewId ReviewId, CommentId CommentId, string Content)
{
    public UpdateCommentCommand(int reviewId, int commentId, string content)
        : this(new ReviewId(reviewId), new CommentId(commentId), content)
    {
    }
}