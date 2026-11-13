using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Entities;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;

public static class CommentResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Comment" /> entity to its <see cref="CommentResource" /> representation.
    /// </summary>
    public static CommentResource ToResourceFromEntity(Comment entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Comment entity cannot be null when converting to resource.");

        return new CommentResource(
            entity.Id,
            entity.ReviewId,
            entity.Content,
            entity.CreatedAt);
    }
}