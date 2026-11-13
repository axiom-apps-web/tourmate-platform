using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Commands;

public record CreateActiveTourCommand(
    TourScheduleId TourScheduleId,
    GuideId GuideId,
    double CurrentLatitude,
    double CurrentLongitude)
{
    public CreateActiveTourCommand(int tourScheduleId, int guideId, double currentLatitude, double currentLongitude)
        : this(new TourScheduleId(tourScheduleId), new GuideId(guideId), currentLatitude, currentLongitude)
    {
    }
}