using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Events;
using Tourmate.Platform.Shared.Application.Internal.EventHandlers;

namespace Tourmate.Platform.FeedbackAndTourReviews.Application.Internal.EventHandlers;

public class ReviewCreatedEventHandler(ILogger<ReviewCreatedEventHandler> logger) : IEventHandler<ReviewCreatedEvent>
{
    /// <inheritdoc />
    public Task Handle(ReviewCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Review created: (UserID: {UserId}, TourID: {TourId})",
            domainEvent.UserId.Value, domainEvent.TourId.Value);

        return Task.CompletedTask;
    }
}