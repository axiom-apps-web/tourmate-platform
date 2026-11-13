using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourMonitoring.Application.CommandServices;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Domain.Model.Commands;
using Tourmate.Platform.TourMonitoring.Domain.Repositories;

namespace Tourmate.Platform.TourMonitoring.Application.Internal.CommandServices;

public class ActiveTourCommandService(
    IActiveTourRepository activeTourRepository,
    IUnitOfWork unitOfWork)
    : IActiveTourCommandService
{
    

    /// <inheritdoc />
    public async Task<Result<ActiveTour>> Handle(CreateActiveTourCommand command, CancellationToken cancellationToken)
    {
        var activeTour = new ActiveTour(command);
        await activeTourRepository.AddAsync(activeTour, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<ActiveTour>.Success(activeTour);
    }
}