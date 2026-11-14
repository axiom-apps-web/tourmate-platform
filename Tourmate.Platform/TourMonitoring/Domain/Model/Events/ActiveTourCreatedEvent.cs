using Tourmate.Platform.Shared.Domain.Model.Events;
using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Events;

public record ActiveTourCreatedEvent(ActiveTourId ActiveTourId, TourScheduleId TourScheduleId, GuideId GuideId): IEvent;