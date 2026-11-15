using Tourmate.Platform.Shared.Application.Internal.EventHandlers;
using Tourmate.Platform.TourManagement.Domain.Model.Events;

namespace Tourmate.Platform.TourManagement.Application.Internal.EventHandlers;

public class TourCreatedEventHandler(ILogger<TourCreatedEventHandler> logger) : IEventHandler<TourCreatedEvent>
{
    public Task Handle(TourCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing TourCreatedEvent for Tour ID {TourId}, with Agency ID {AgencyId} and Title {Title}", 
            domainEvent.TourId.Value, domainEvent.AgencyId.Value, domainEvent.Title);

        return Task.CompletedTask;
    }
}