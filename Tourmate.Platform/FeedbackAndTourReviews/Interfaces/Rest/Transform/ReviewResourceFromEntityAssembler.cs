using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;

public static class ReviewResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Review" /> domain aggregate to its <see cref="ReviewResource" /> representation.
    /// </summary>
    public static ReviewResource ToResourceFromEntity(Review entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Review entity cannot be null when converting to resource.");

        return new ReviewResource(
            entity.Id,
            entity.UserId,
            entity.TourId,
            entity.Rating.Value,
            entity.CreatedAt,
            entity.Comments.Select(CommentResourceFromEntityAssembler.ToResourceFromEntity));
    }
}