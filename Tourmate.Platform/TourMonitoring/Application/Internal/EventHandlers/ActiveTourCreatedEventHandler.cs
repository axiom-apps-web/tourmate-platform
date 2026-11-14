using Tourmate.Platform.Shared.Application.Internal.EventHandlers;
using Tourmate.Platform.TourMonitoring.Domain.Model.Events;

namespace Tourmate.Platform.TourMonitoring.Application.Internal.EventHandlers;

public class ActiveTourCreatedEventHandler(ILogger<ActiveTourCreatedEventHandler> logger) : IEventHandler<ActiveTourCreatedEvent>
{
    /// <inheritdoc />
    public Task Handle(ActiveTourCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("ActiveTour created:  (ID: {ActiveTourId}, TourScheduleID: {TourScheduleId}, GuideID: {GuideId})",
             domainEvent.ActiveTourId.Value, domainEvent.TourScheduleId.Value, domainEvent.GuideId.Value);

        return Task.CompletedTask;
    }
}