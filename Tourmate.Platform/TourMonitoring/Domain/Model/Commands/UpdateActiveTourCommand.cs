using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Commands;

public record UpdateActiveTourCommand(
    ActiveTourId ActiveTourId,
    TourScheduleId TourScheduleId,
    GuideId GuideId,
    double CurrentLatitude,
    double CurrentLongitude)
{
    public UpdateActiveTourCommand(int activeTourId, int tourScheduleId, int guideId, double currentLatitude, double currentLongitude)
        : this(new ActiveTourId(activeTourId), new TourScheduleId(tourScheduleId), new GuideId(guideId), currentLatitude, currentLongitude)
    {
    }
}