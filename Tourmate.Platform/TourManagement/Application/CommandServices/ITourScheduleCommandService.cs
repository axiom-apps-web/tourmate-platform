using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;

namespace Tourmate.Platform.TourManagement.Application.CommandService;

public interface ITourScheduleCommandService
{
    Task<Result<TourSchedule>> Handle(CreateTourScheduleCommand command, CancellationToken cancellationToken);
}