using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.QueryServices;

public class IncidentQueryService(IIncidentRepository incidentRepository)
{
    public async Task<IEnumerable<Incident>> HandleGetAllAsync(CancellationToken cancellationToken)
    {
        return await incidentRepository.ListAsync(cancellationToken);
    }

    public async Task<Incident?> HandleGetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await incidentRepository.FindByIdAsync(id, cancellationToken);
    }
}