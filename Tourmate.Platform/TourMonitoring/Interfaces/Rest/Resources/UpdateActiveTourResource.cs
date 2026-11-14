namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;

public record UpdateActiveTourResource(
    int TourScheduleId, 
    int GuideId,
    double CurrentLatitude,
    double CurrentLongitude);