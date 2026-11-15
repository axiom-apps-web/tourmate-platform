namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

public record CreateTourResource(
    int AgencyId,
    string Title,
    string Description,
    string Duration,
    string DifficultyLevel,
    double Amount,
    string Currency
    );