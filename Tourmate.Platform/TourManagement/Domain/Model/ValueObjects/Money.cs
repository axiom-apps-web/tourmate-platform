namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public record Money(double Amount, string Currency)
{
    public Money() : this(0.0, string.Empty)
    {
    }
}