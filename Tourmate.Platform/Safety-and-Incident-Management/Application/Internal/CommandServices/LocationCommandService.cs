using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.CommandServices;

public class LocationCommandService(
    ILocationRepository locationRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<LocationRecord> HandleCreateAsync(
        int? incidentId, int expeditionId, int touristId,
        string latitude, string longitude, DateTimeOffset recordedAt,
        string expectedLatitude, string expectedLongitude,
        string alerts, string telemetry, string wearables,
        CancellationToken cancellationToken)
    {
        var record = new LocationRecord(
            incidentId, expeditionId, touristId, latitude, longitude,
            recordedAt, expectedLatitude, expectedLongitude, alerts, telemetry, wearables
        );

        await locationRepository.AddAsync(record, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return record;
    }

    public async Task<LocationRecord?> HandleUpdateAsync(
        int id, int? incidentId, int expeditionId, int touristId,
        string latitude, string longitude,
        string expectedLatitude, string expectedLongitude,
        string alerts, string telemetry, string wearables,
        CancellationToken cancellationToken)
    {
        var record = await locationRepository.FindByIdAsync(id, cancellationToken);
        if (record == null) return null;

        record.UpdateRecord(incidentId, expeditionId, touristId, latitude, longitude, expectedLatitude, expectedLongitude, alerts, telemetry, wearables);
        locationRepository.Update(record);
        await unitOfWork.CompleteAsync(cancellationToken);
        return record;
    }

    public async Task<bool> HandleDeleteAsync(int id, CancellationToken cancellationToken)
    {
        var record = await locationRepository.FindByIdAsync(id, cancellationToken);
        if (record == null) return false;

        locationRepository.Remove(record);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}