using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Domain.Model.Commands;

namespace Tourmate.Platform.TourMonitoring.Application.CommandServices;

public interface IActiveTourCommandService
{
    /// <summary>
    ///     Handles the create active tour command in the Tour Mate Platform.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateActiveTourCommand" /> command to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The created <see cref="ActiveTour" /> entity.
    /// </returns>
    Task<Result<ActiveTour>> Handle(CreateActiveTourCommand command, CancellationToken cancellationToken);
}