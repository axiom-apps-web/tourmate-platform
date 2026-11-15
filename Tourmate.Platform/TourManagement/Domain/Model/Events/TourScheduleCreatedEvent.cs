using Tourmate.Platform.Shared.Domain.Model.Events;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Events;

public record TourScheduleCreatedEvent(TourScheduleId TourScheduleId, DepartureDateTime DepartureDateTime, int MaxCapacity) : IEvent;