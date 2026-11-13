using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;

public record DeleteCommentCommand(ReviewId ReviewId, CommentId CommentId)
{
    public DeleteCommentCommand(int reviewId, int commentId) : this(new ReviewId(reviewId), new CommentId(commentId))
    {
    }
}