using Tourmate.Platform.TourMonitoring.Application.QueryServices;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Domain.Model.Queries;
using Tourmate.Platform.TourMonitoring.Domain.Repositories;

namespace Tourmate.Platform.TourMonitoring.Application.Internal.QueryServices;

/// <summary>
///     ActiveTour query service
/// </summary>
/// <param name="activeTourRepository">
///     ActiveTour repository
/// </param>
public class ActiveTourQueryService(IActiveTourRepository activeTourRepository) : IActiveTourQueryService

{
    /// <inheritdoc />
    public async Task<ActiveTour?> Handle(GetActiveTourByIdQuery query, CancellationToken cancellationToken)
    {
        return await activeTourRepository.FindByIdAsNoTrackingAsync(query.ActiveTourId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ActiveTour>> Handle(GetAllActiveToursQuery query, CancellationToken cancellationToken)
    {
        return await activeTourRepository.ListAsNoTrackingAsync(cancellationToken);
    }

   
}