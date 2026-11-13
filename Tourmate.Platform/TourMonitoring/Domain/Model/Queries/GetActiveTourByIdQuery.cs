using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Queries;

public record GetActiveTourByIdQuery(ActiveTourId Id)
{
    public GetActiveTourByIdQuery(int activeTourId) : this(new ActiveTourId(activeTourId))
    {
    }
}