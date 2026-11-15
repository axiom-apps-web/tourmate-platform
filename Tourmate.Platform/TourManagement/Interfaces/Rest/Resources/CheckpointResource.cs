namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

public record CheckpointResource(
    int Id,
    string Name,
    double Xaxis,
    double Yaxis,
    int Order
    );