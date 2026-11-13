using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;

public static class CommentCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CommentContentResource" /> to a <see cref="AddCommentToReviewCommand" />.
    /// </summary>
    public static AddCommentToReviewCommand ToAddCommandFromResource(int reviewId, CommentContentResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CommentContentResource cannot be null when converting to command.");

        return new AddCommentToReviewCommand(reviewId, resource.Content);
    }

    /// <summary>
    ///     Converts a <see cref="CommentContentResource" /> to a <see cref="UpdateCommentCommand" />.
    /// </summary>
    public static UpdateCommentCommand ToUpdateCommandFromResource(int reviewId, int commentId,
        CommentContentResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CommentContentResource cannot be null when converting to command.");

        return new UpdateCommentCommand(reviewId, commentId, resource.Content);
    }
}