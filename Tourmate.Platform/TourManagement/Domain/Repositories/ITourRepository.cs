
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Repositories;

public interface ITourRepository : IBaseRepository<Tour>
{
    Task<Tour?> FindByIdAsNoTrackingAsync(TourId id, CancellationToken cancellationToken);
    Task<List<Tour>> FindAllAsNoTrackingAsync(CancellationToken cancellationToken);
}