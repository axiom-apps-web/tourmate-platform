using Microsoft.AspNetCore.Mvc;
using Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.CommandServices;
using Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.QueryServices;
using Tourmate.Platform.SafetyAndIncidentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/incidents")]
public class IncidentsController(
    IncidentCommandService commandService,
    IncidentQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncidentResource>>> GetAllIncidents(CancellationToken cancellationToken)
    {
        var incidents = await queryService.HandleGetAllAsync(cancellationToken);
        var resources = incidents.Select(i => new IncidentResource(
            i.Id, i.Uuid, i.ActiveTourId, i.ReportedByUserId, i.Description,
            i.Latitude, i.Longitude, i.ReportedAt, i.Status, i.Severity,
            i.ResolutionNote, i.ReporterRole
        ));
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IncidentResource>> GetIncidentById(int id, CancellationToken cancellationToken)
    {
        var incident = await queryService.HandleGetByIdAsync(id, cancellationToken);
        if (incident == null) return NotFound();

        var resource = new IncidentResource(
            incident.Id, incident.Uuid, incident.ActiveTourId, incident.ReportedByUserId, incident.Description,
            incident.Latitude, incident.Longitude, incident.ReportedAt, incident.Status, incident.Severity,
            incident.ResolutionNote, incident.ReporterRole
        );
        return Ok(resource);
    }

    [HttpPost]
    public async Task<ActionResult<IncidentResource>> CreateIncident([FromBody] CreateIncidentResource resource, CancellationToken cancellationToken)
    {
        var reportedAt = ParseReportedAt(resource.ReportedAt);
        var status = string.IsNullOrWhiteSpace(resource.Status) ? "OPEN" : resource.Status.Trim();
        var severity = string.IsNullOrWhiteSpace(resource.Severity) ? "MEDIUM" : resource.Severity.Trim();
        var resolutionNote = string.IsNullOrWhiteSpace(resource.ResolutionNote) ? string.Empty : resource.ResolutionNote.Trim();
        var reporterRole = string.IsNullOrWhiteSpace(resource.ReporterRole) ? "TOURIST" : resource.ReporterRole.Trim();
        var uuid = string.IsNullOrWhiteSpace(resource.Uuid) ? Guid.NewGuid().ToString() : resource.Uuid.Trim();
        var description = string.IsNullOrWhiteSpace(resource.Description) ? "Incident reported." : resource.Description.Trim();

        var incident = await commandService.HandleCreateAsync(
            uuid, resource.ActiveTourId, resource.ReportedByUserId, description,
            resource.Latitude, resource.Longitude, reportedAt, status,
            severity, resolutionNote, reporterRole, cancellationToken
        );

        var responseResource = new IncidentResource(
            incident.Id, incident.Uuid, incident.ActiveTourId, incident.ReportedByUserId, incident.Description,
            incident.Latitude, incident.Longitude, incident.ReportedAt, incident.Status, incident.Severity,
            incident.ResolutionNote, incident.ReporterRole
        );
        return CreatedAtAction(nameof(GetIncidentById), new { id = incident.Id }, responseResource);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<IncidentResource>> UpdateIncident(int id, [FromBody] UpdateIncidentResource resource, CancellationToken cancellationToken)
    {
        var existingIncident = await queryService.HandleGetByIdAsync(id, cancellationToken);
        if (existingIncident == null) return NotFound();

        var status = string.IsNullOrWhiteSpace(resource.Status) ? existingIncident.Status : resource.Status.Trim();
        var severity = string.IsNullOrWhiteSpace(resource.Severity) ? existingIncident.Severity : resource.Severity.Trim();
        var resolutionNote = string.IsNullOrWhiteSpace(resource.ResolutionNote) ? existingIncident.ResolutionNote : resource.ResolutionNote.Trim();
        var reporterRole = string.IsNullOrWhiteSpace(resource.ReporterRole) ? existingIncident.ReporterRole : resource.ReporterRole.Trim();
        var description = string.IsNullOrWhiteSpace(resource.Description) ? existingIncident.Description : resource.Description.Trim();

        var incident = await commandService.HandleUpdateAsync(
            id, description, resource.Latitude, resource.Longitude,
            status, severity, resolutionNote, reporterRole, cancellationToken
        );
        if (incident == null) return NotFound();

        var responseResource = new IncidentResource(
            incident.Id, incident.Uuid, incident.ActiveTourId, incident.ReportedByUserId, incident.Description,
            incident.Latitude, incident.Longitude, incident.ReportedAt, incident.Status, incident.Severity,
            incident.ResolutionNote, incident.ReporterRole
        );
        return Ok(responseResource);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteIncident(int id, CancellationToken cancellationToken)
    {
        var deleted = await commandService.HandleDeleteAsync(id, cancellationToken);
        if (!deleted) return NotFound();

        return NoContent();
    }

    private static DateTimeOffset ParseReportedAt(string? reportedAtValue)
    {
        if (string.IsNullOrWhiteSpace(reportedAtValue))
        {
            return DateTimeOffset.UtcNow;
        }

        return DateTimeOffset.TryParse(reportedAtValue, out var parsed)
            ? parsed
            : DateTimeOffset.UtcNow;
    }
}