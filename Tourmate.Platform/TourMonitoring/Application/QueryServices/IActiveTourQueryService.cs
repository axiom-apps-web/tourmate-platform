using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Domain.Model.Queries;

namespace Tourmate.Platform.TourMonitoring.Application.QueryServices;

public interface IActiveTourQueryService
{
    /// <summary>
    ///     Handles the get active tour by id query in the Tour Mate Platform.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetActiveTourByIdQuery" /> query to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="ActiveTour" /> entity.
    /// </returns>
    Task<ActiveTour?> Handle(GetActiveTourByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the get all active tours query in the Tour Mate Platform.       
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetAllActiveToursQuery" /> query to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A collection of all active tours in the platform.
    /// </returns>
    Task<IEnumerable<ActiveTour>> Handle(GetAllActiveToursQuery query, CancellationToken cancellationToken);
}