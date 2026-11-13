using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;

public static class UpdateReviewRatingCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="UpdateReviewRatingResource" /> to a <see cref="UpdateReviewRatingCommand" />.
    /// </summary>
    public static UpdateReviewRatingCommand ToCommandFromResource(int reviewId, UpdateReviewRatingResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "UpdateReviewRatingResource cannot be null when converting to command.");

        return new UpdateReviewRatingCommand(reviewId, resource.Rating);
    }
}