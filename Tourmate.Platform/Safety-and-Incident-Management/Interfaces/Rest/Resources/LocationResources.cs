namespace Tourmate.Platform.SafetyAndIncidentManagement.Interfaces.Rest.Resources;

public record LocationResource(
    int Id,
    int? IncidentId,
    int ExpeditionId,
    int TouristId,
    string Latitude,
    string Longitude,
    DateTimeOffset RecordedAt,
    string ExpectedLatitude,
    string ExpectedLongitude,
    string Alerts,
    string Telemetry,
    string Wearables
);

public record SaveLocationResource(
    int? IncidentId,
    int ExpeditionId,
    int TouristId,
    string Latitude,
    string Longitude,
    DateTimeOffset? RecordedAt,
    string ExpectedLatitude,
    string ExpectedLongitude,
    string Alerts,
    string Telemetry,
    string Wearables
);