using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;
using Tourmate.Platform.TourMonitoring.Domain.Repositories;

namespace Tourmate.Platform.TourMonitoring.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Repositories;


/// <summary>
///     Represents the active tour repository in the TourMonitoring Platform.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class ActiveTourRepository(AppDbContext context) : BaseRepository<ActiveTour>(context), IActiveTourRepository
{
    

    /// <inheritdoc />
    public override async Task<ActiveTour?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ActiveTour>()
            .Include(activeTour => activeTour.TourScheduleId)
            .Include(activeTour => activeTour.GuideId)
            .FirstOrDefaultAsync(activeTour => activeTour.Id == id, cancellationToken);
    }
    

    /// <inheritdoc />
    public override async Task<ActiveTour?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ActiveTour>()
            .AsNoTracking()
            .AsSplitQuery()
            .Include(activeTour => activeTour.TourScheduleId)
            .Include(activeTour => activeTour.GuideId)
            .FirstOrDefaultAsync(activeTour => activeTour.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ActiveTour>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<ActiveTour>()
            .Include(activeTour => activeTour.TourScheduleId)
            .Include(activeTour => activeTour.GuideId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ActiveTour>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<ActiveTour>()
            .AsNoTracking()
            .AsSplitQuery()
            .Include(activeTour => activeTour.TourScheduleId)
            .Include(activeTour => activeTour.GuideId)
            .ToListAsync(cancellationToken);
    }
}