using Tourmate.Platform.Shared.Application.Internal.EventHandlers;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Events;

namespace Tourmate.Platform.TourManagement.Application.Internal.EventHandlers;

public class TourScheduleCreatedEventHandler(ILogger<TourScheduleCreatedEventHandler> logger) : IEventHandler<TourScheduleCreatedEvent>
{
    public Task Handle(TourScheduleCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing TourCreatedEvent for TourSchedule ID {TourScheduleId}, with Departure DateTime {DepartureDateTime} and Max capacity {MaxCapacity}", 
            domainEvent.TourScheduleId, domainEvent.DepartureDateTime, domainEvent.MaxCapacity);

        return Task.CompletedTask;
    }
}