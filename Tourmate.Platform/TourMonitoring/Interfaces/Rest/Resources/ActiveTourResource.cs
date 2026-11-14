namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;

public record ActiveTourResource(
    int Id,
    int TourScheduleId,
    int GuideId,
    string Status,
    double CurrentLatitude,
    double CurrentLongitude,
    DateTimeOffset StartedAt,
    DateTimeOffset? FinishedAt
    );