using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourManagement.Application.QueryService;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Queries;
using Tourmate.Platform.TourManagement.Domain.Repositories;

namespace Tourmate.Platform.TourManagement.Application.Internal.QueryServices;

public class TourQueryService(
    ITourRepository tourRepository,
    IUnitOfWork unitOfWork) : ITourQueryService
{
    public async Task<List<Tour>> Handle(GetAllToursQuery query, CancellationToken cancellationToken)
    {
        return await tourRepository.FindAllAsNoTrackingAsync(cancellationToken);
    }

    public async Task<Tour?> Handle(GetTourByIdQuery query, CancellationToken cancellationToken)
    {
        return await tourRepository.FindByIdAsNoTrackingAsync(query.TourId, cancellationToken);
    }
}