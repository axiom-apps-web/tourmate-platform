using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Commands;

public record DeleteActiveTourCommand(ActiveTourId ActiveTourId)
{
    public DeleteActiveTourCommand(int activeTourId)
        : this(new ActiveTourId(activeTourId))
    {
    }
}