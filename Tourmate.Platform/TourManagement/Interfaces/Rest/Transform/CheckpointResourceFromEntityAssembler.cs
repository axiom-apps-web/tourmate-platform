using Tourmate.Platform.TourManagement.Domain.Model.Entities;
using Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Transform;

public static class CheckpointResourceFromEntityAssembler
{
    public static CheckpointResource ToResourceFromEntity(Checkpoint entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity),
                "CheckpointResource entity cannot be null when converting to resource");
        }

        return new CheckpointResource(
            entity.Id,
            entity.Name,
            entity.Coordinates.Xaxis,
            entity.Coordinates.Yaxis,
            entity.Order
        );
    }
}