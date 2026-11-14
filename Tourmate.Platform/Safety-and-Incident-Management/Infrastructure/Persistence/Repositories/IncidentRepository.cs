using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Infrastructure.Persistence.Repositories;

public class IncidentRepository(AppDbContext context) : BaseRepository<Incident>(context), IIncidentRepository
{
    public async Task<IEnumerable<Incident>> FindByActiveTourIdAsync(int activeTourId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Incident>()
            .Where(i => i.ActiveTourId == activeTourId)
            .ToListAsync(cancellationToken);
    }
}