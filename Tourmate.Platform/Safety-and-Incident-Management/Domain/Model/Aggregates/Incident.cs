using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.Shared.Domain.Model.Entities;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;

public class Incident : AggregateRoot, IAuditableEntity
{
    public Incident()
    {
        Uuid = string.Empty;
        Description = string.Empty;
        Status = "OPEN";
        Severity = "MEDIUM";
        ResolutionNote = string.Empty;
        ReporterRole = "TOURIST";
        ReportedAt = DateTimeOffset.UtcNow;
    }

    public Incident(
        string uuid,
        int activeTourId,
        int reportedByUserId,
        string description,
        double latitude,
        double longitude,
        DateTimeOffset reportedAt,
        string status,
        string severity,
        string resolutionNote,
        string reporterRole)
    {
        Uuid = uuid;
        ActiveTourId = activeTourId;
        ReportedByUserId = reportedByUserId;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        ReportedAt = reportedAt;
        Status = status;
        Severity = severity;
        ResolutionNote = resolutionNote;
        ReporterRole = reporterRole;
    }

    public int Id { get; private set; }
    public string Uuid { get; private set; }
    public int ActiveTourId { get; private set; }
    public int ReportedByUserId { get; private set; }
    public string Description { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public DateTimeOffset ReportedAt { get; private set; }
    public string Status { get; private set; }
    public string Severity { get; private set; }
    public string ResolutionNote { get; private set; }
    public string ReporterRole { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void UpdateDetails(
        string description,
        double latitude,
        double longitude,
        string status,
        string severity,
        string resolutionNote,
        string reporterRole)
    {
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        Status = status;
        Severity = severity;
        ResolutionNote = resolutionNote;
        ReporterRole = reporterRole;
    }
}