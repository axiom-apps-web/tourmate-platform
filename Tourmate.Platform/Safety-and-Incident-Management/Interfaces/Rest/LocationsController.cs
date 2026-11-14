using Microsoft.AspNetCore.Mvc;
using Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.CommandServices;
using Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.QueryServices;
using Tourmate.Platform.SafetyAndIncidentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/locations")]
public class LocationsController(
    LocationCommandService commandService,
    LocationQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationResource>>> GetAllLocations(CancellationToken cancellationToken)
    {
        var records = await queryService.HandleGetAllAsync(cancellationToken);
        var resources = records.Select(r => new LocationResource(
            r.Id, r.IncidentId, r.ExpeditionId, r.TouristId,
            r.Latitude, r.Longitude, r.RecordedAt,
            r.ExpectedLatitude, r.ExpectedLongitude,
            r.Alerts, r.Telemetry, r.Wearables
        ));
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LocationResource>> GetLocationById(int id, CancellationToken cancellationToken)
    {
        var record = await queryService.HandleGetByIdAsync(id, cancellationToken);
        if (record == null) return NotFound();

        var resource = new LocationResource(
            record.Id, record.IncidentId, record.ExpeditionId, record.TouristId,
            record.Latitude, record.Longitude, record.RecordedAt,
            record.ExpectedLatitude, record.ExpectedLongitude,
            record.Alerts, record.Telemetry, record.Wearables
        );
        return Ok(resource);
    }

    [HttpPost]
    public async Task<ActionResult<LocationResource>> CreateLocation([FromBody] SaveLocationResource resource, CancellationToken cancellationToken)
    {
        var record = await commandService.HandleCreateAsync(
            resource.IncidentId, resource.ExpeditionId, resource.TouristId,
            resource.Latitude, resource.Longitude, resource.RecordedAt ?? DateTimeOffset.UtcNow,
            resource.ExpectedLatitude ?? string.Empty, resource.ExpectedLongitude ?? string.Empty,
            resource.Alerts ?? string.Empty, resource.Telemetry ?? string.Empty, resource.Wearables ?? string.Empty,
            cancellationToken
        );

        var responseResource = new LocationResource(
            record.Id, record.IncidentId, record.ExpeditionId, record.TouristId,
            record.Latitude, record.Longitude, record.RecordedAt,
            record.ExpectedLatitude, record.ExpectedLongitude,
            record.Alerts, record.Telemetry, record.Wearables
        );
        return CreatedAtAction(nameof(GetLocationById), new { id = record.Id }, responseResource);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<LocationResource>> UpdateLocation(int id, [FromBody] SaveLocationResource resource, CancellationToken cancellationToken)
    {
        var record = await commandService.HandleUpdateAsync(
            id, resource.IncidentId, resource.ExpeditionId, resource.TouristId,
            resource.Latitude, resource.Longitude,
            resource.ExpectedLatitude ?? string.Empty, resource.ExpectedLongitude ?? string.Empty,
            resource.Alerts ?? string.Empty, resource.Telemetry ?? string.Empty, resource.Wearables ?? string.Empty,
            cancellationToken
        );
        if (record == null) return NotFound();

        var responseResource = new LocationResource(
            record.Id, record.IncidentId, record.ExpeditionId, record.TouristId,
            record.Latitude, record.Longitude, record.RecordedAt,
            record.ExpectedLatitude, record.ExpectedLongitude,
            record.Alerts, record.Telemetry, record.Wearables
        );
        return Ok(responseResource);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteLocation(int id, CancellationToken cancellationToken)
    {
        var deleted = await commandService.HandleDeleteAsync(id, cancellationToken);
        if (!deleted) return NotFound();

        return NoContent();
    }
}