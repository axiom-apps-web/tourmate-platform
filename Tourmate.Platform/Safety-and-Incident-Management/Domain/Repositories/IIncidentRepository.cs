using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;

public interface IIncidentRepository : IBaseRepository<Incident>
{
    Task<IEnumerable<Incident>> FindByActiveTourIdAsync(int activeTourId, CancellationToken cancellationToken = default);
}