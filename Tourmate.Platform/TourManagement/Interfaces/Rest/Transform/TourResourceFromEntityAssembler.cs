using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Transform;

public static class TourResourceFromEntityAssembler
{
    public static TourResource ToResourceFromEntity(Tour entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity),
                "TourResource entity cannot be null when converting to resource");
        }
        
        var checkpoints = entity.Checkpoints.Select(CheckpointResourceFromEntityAssembler.ToResourceFromEntity).ToList();

        return new TourResource(
            entity.Id,
            entity.AgencyId,
            entity.Details.Title,
            entity.Status.ToString(),
            checkpoints
        );
    }
}