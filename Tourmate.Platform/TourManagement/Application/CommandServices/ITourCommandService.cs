using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;

namespace Tourmate.Platform.TourManagement.Application.CommandService;

public interface ITourCommandService
{
    Task<Result<Tour>> Handle(CreateTourCommand command, CancellationToken cancellationToken);
    
    Task<Result<Tour>> Handle(TourId tourId, UpdateTourCommand command, CancellationToken cancellationToken);
    
    Task<Result> Handle(DeleteTourCommand command, CancellationToken cancellationToken);
    
}