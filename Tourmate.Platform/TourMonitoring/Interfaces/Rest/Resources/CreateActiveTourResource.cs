namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;

public record CreateActiveTourResource(
    int TourScheduleId, 
    int GuideId,
    string Status,
    double CurrentLatitude,
    double CurrentLongitude,
    DateTimeOffset StartedAt);