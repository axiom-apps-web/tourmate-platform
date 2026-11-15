using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.TourManagement.Domain.Repositories;

namespace Tourmate.Platform.TourManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TourRepository(AppDbContext context)
: BaseRepository<Tour>(context), ITourRepository
{
    public async Task<Tour?> FindByIdAsNoTrackingAsync(TourId id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Tour>()
            .AsNoTracking()
            .Include(t => t.Checkpoints)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Tour>> FindAllAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Tour>()
            .AsNoTracking()
            .Include(t => t.Checkpoints)
            .ToListAsync(cancellationToken);
    }
}