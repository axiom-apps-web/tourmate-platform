namespace Tourmate.Platform.SafetyAndIncidentManagement.Interfaces.Rest.Resources;

public record IncidentResource(
    int Id,
    string Uuid,
    int ActiveTourId,
    int ReportedByUserId,
    string Description,
    double Latitude,
    double Longitude,
    DateTimeOffset ReportedAt,
    string Status,
    string Severity,
    string ResolutionNote,
    string ReporterRole
);

public record CreateIncidentResource(
    string? Uuid,
    int ActiveTourId,
    int ReportedByUserId,
    string? Description,
    double Latitude,
    double Longitude,
    string? ReportedAt,
    string? Status,
    string? Severity,
    string? ResolutionNote,
    string? ReporterRole
);

public record UpdateIncidentResource(
    int Id,
    string? Uuid,
    int ActiveTourId,
    int ReportedByUserId,
    string? Description,
    double Latitude,
    double Longitude,
    string? ReportedAt,
    string? Status,
    string? Severity,
    string? ResolutionNote,
    string? ReporterRole
);