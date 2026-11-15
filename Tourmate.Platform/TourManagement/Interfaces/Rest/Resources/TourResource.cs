namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

public record TourResource(
    int Id,
    int AgencyId,
    string Title,
    string Status,
    List<CheckpointResource> Checkpoints
    );