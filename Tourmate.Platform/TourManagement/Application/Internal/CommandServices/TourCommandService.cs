using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.TourManagement.Application.CommandService;
using Tourmate.Platform.TourManagement.Domain.Model;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Model.Errors;
using Tourmate.Platform.TourManagement.Domain.Repositories;

namespace Tourmate.Platform.TourManagement.Application.Internal.CommandServices;

public class TourCommandService(
    ITourRepository tourRepository,
    IUnitOfWork unitOfWork):  ITourCommandService
{
    public async Task<Result<Tour>> Handle(CreateTourCommand command, CancellationToken cancellationToken)
    {
        var tour = new Tour(command);
        await tourRepository.AddAsync(tour, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Tour>.Success(tour);
    }

    public async Task<Result<Tour>> Handle(TourId tourId, UpdateTourCommand command, CancellationToken cancellationToken)
    {
        var tour = await tourRepository.FindByIdAsync(tourId, cancellationToken);
        if (tour is null)
            return Result<Tour>.Failure(
                ETourManagementError.TourNotFound,
                "The specified active tour was not found.");
        
        tour.ChangeDetails(command.Title,command.Description,command.Duration,command.DifficultyLevel, command.Amount,command.Currency);
        
        tourRepository.Update(tour);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Tour>.Success(tour);
    }

    public async Task<Result> Handle(DeleteTourCommand command, CancellationToken cancellationToken)
    {
        var tour = await tourRepository.FindByIdAsync(command.Id, cancellationToken);
        if (tour is null)
            return Result.Failure(
                ETourManagementError.TourNotFound,
                "The specified active tour was not found.");

        tourRepository.Remove(tour);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
    
    
}