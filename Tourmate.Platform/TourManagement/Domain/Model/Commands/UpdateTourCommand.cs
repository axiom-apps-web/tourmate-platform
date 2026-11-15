using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Commands;

public record UpdateTourCommand(
    string Title,
    string Description,
    string Duration,
    EDifficultyLevel DifficultyLevel,
    double Amount,
    string Currency
    );