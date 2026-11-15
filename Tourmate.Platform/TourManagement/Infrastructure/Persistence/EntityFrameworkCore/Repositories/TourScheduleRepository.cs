using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.TourManagement.Domain.Repositories;

namespace Tourmate.Platform.TourManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TourScheduleRepository(AppDbContext context)
: BaseRepository<TourSchedule>(context), ITourScheduleRepository
{
    public async Task<TourSchedule?> FindByIdAsNoTrackingAsync(TourScheduleId id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TourSchedule>()
            .AsNoTracking()
            .Include(t => t.Participants)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<TourSchedule>> FindAllAsNoTrackingAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<TourSchedule>()
            .AsNoTracking()
            .Include(t => t.Participants)
            .ToListAsync(cancellationToken);
    }
}