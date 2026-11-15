using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Commands;

public record CreateTourCommand(
    AgencyId AgencyId,
    string Title,
    string Description,
    string Duration,
    EDifficultyLevel DifficultyLevel,
    double Amount,
    string Currency
    );