using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.QueryServices;

public class LocationQueryService(ILocationRepository locationRepository)
{
    public async Task<IEnumerable<LocationRecord>> HandleGetAllAsync(CancellationToken cancellationToken)
    {
        return await locationRepository.ListAsync(cancellationToken);
    }

    public async Task<LocationRecord?> HandleGetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await locationRepository.FindByIdAsync(id, cancellationToken);
    }
}