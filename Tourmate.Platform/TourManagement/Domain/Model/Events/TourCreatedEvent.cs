using Tourmate.Platform.Shared.Domain.Model.Events;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;


namespace Tourmate.Platform.TourManagement.Domain.Model.Events;

public record TourCreatedEvent(TourId TourId, AgencyId AgencyId, string Title) : IEvent;