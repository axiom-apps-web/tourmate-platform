using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Repositories;

public interface ITourScheduleRepository : IBaseRepository<TourSchedule>
{
    Task<TourSchedule?> FindByIdAsNoTrackingAsync(TourScheduleId id, CancellationToken cancellationToken);
    Task<List<TourSchedule>> FindAllAsNoTrackingAsync(CancellationToken cancellationToken);
}