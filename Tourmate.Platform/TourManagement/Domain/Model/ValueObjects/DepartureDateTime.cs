namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public record DepartureDateTime(DateTimeOffset Value)
{
    public DepartureDateTime() : this(DateTimeOffset.UtcNow)
    {
    }
}