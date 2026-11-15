using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourManagement.Application.QueryService;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Queries;
using Tourmate.Platform.TourManagement.Domain.Repositories;

namespace Tourmate.Platform.TourManagement.Application.Internal.QueryServices;

public class TourScheduleQueryService(
    ITourScheduleRepository tourScheduleRepository,
    IUnitOfWork unitOfWork) : ITourScheduleQueryService
{
    public async Task<List<TourSchedule>> Handle(GetAllTourSchedulesQuery query, CancellationToken cancellationToken)
    {
        return await tourScheduleRepository.FindAllAsNoTrackingAsync(cancellationToken);
    }

    public async  Task<TourSchedule?> Handle(GetTourScheduleByIdQuery query, CancellationToken cancellationToken)
    {
        return await tourScheduleRepository.FindByIdAsNoTrackingAsync(query.TourScheduleId, cancellationToken);
    }
}