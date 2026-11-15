using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Queries;

namespace Tourmate.Platform.TourManagement.Application.QueryService;

public interface ITourScheduleQueryService
{
    Task<List<TourSchedule>> Handle(GetAllTourSchedulesQuery query, CancellationToken cancellationToken);
    
    Task<TourSchedule?> Handle(GetTourScheduleByIdQuery query, CancellationToken cancellationToken);
}