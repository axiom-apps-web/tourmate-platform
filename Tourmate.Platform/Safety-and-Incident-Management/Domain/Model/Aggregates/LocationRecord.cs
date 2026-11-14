using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.Shared.Domain.Model.Entities;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;

public class LocationRecord : AggregateRoot, IAuditableEntity
{
    public LocationRecord()
    {
        Latitude = string.Empty;
        Longitude = string.Empty;
        RecordedAt = DateTimeOffset.UtcNow;
        ExpectedLatitude = string.Empty;
        ExpectedLongitude = string.Empty;
        Alerts = string.Empty;
        Telemetry = string.Empty;
        Wearables = string.Empty;
    }

    public LocationRecord(
        int? incidentId,
        int expeditionId,
        int touristId,
        string latitude,
        string longitude,
        DateTimeOffset recordedAt,
        string expectedLatitude,
        string expectedLongitude,
        string alerts,
        string telemetry,
        string wearables)
    {
        IncidentId = incidentId;
        ExpeditionId = expeditionId;
        TouristId = touristId;
        Latitude = latitude;
        Longitude = longitude;
        RecordedAt = recordedAt;
        ExpectedLatitude = expectedLatitude;
        ExpectedLongitude = expectedLongitude;
        Alerts = alerts;
        Telemetry = telemetry;
        Wearables = wearables;
    }

    public int Id { get; private set; }
    public int? IncidentId { get; private set; }
    public int ExpeditionId { get; private set; }
    public int TouristId { get; private set; }
    public string Latitude { get; private set; }
    public string Longitude { get; private set; }
    public DateTimeOffset RecordedAt { get; private set; }
    public string ExpectedLatitude { get; private set; }
    public string ExpectedLongitude { get; private set; }
    public string Alerts { get; private set; }
    public string Telemetry { get; private set; }
    public string Wearables { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void UpdateRecord(
        int? incidentId,
        int expeditionId,
        int touristId,
        string latitude,
        string longitude,
        string expectedLatitude,
        string expectedLongitude,
        string alerts,
        string telemetry,
        string wearables)
    {
        IncidentId = incidentId;
        ExpeditionId = expeditionId;
        TouristId = touristId;
        Latitude = latitude;
        Longitude = longitude;
        ExpectedLatitude = expectedLatitude;
        ExpectedLongitude = expectedLongitude;
        Alerts = alerts;
        Telemetry = telemetry;
        Wearables = wearables;
    }
}