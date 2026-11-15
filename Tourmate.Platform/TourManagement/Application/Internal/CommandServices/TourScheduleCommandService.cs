using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourManagement.Application.CommandService;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Repositories;

namespace Tourmate.Platform.TourManagement.Application.Internal.CommandServices;

public class TourScheduleCommandService(
    ITourScheduleRepository tourScheduleRepository,
    IUnitOfWork unitOfWork) : ITourScheduleCommandService
{
    public async Task<Result<TourSchedule>> Handle(CreateTourScheduleCommand command, CancellationToken cancellationToken)
    {
        var tourSchedule = new TourSchedule(command);
        await tourScheduleRepository.AddAsync(tourSchedule);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<TourSchedule>.Success(tourSchedule);
    }
}