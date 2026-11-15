namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public record Coordinates(double Xaxis, double Yaxis)
{
    public Coordinates() : this(0, 0)
    {
    }
}