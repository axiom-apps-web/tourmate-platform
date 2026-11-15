using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Queries;

namespace Tourmate.Platform.TourManagement.Application.QueryService;

public interface ITourQueryService
{
    Task<List<Tour>> Handle(GetAllToursQuery query, CancellationToken cancellationToken);
    
    Task<Tour?> Handle(GetTourByIdQuery query, CancellationToken cancellationToken);
}