namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public record TourDetails(
    string Title,
    string Description,
    string Duration,
    EDifficultyLevel DifficultyLevel
    )
{
    public TourDetails() : this(string.Empty, string.Empty, string.Empty,
        default)
    {
    }
}