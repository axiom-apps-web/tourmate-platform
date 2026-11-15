namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public record Currency(string Code)
{
    public Currency() : this(string.Empty)
    {
    }
}