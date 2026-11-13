using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;

public static class CreateReviewCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateReviewResource" /> to a <see cref="CreateReviewCommand" />.
    /// </summary>
    public static CreateReviewCommand ToCommandFromResource(CreateReviewResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateReviewResource cannot be null when converting to command.");

        return new CreateReviewCommand(
            resource.UserId,
            resource.TourId,
            resource.Rating,
            resource.Comment);
    }
}