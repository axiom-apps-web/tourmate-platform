using Tourmate.Platform.TourMonitoring.Domain.Model.Commands;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Transform;

public static class UpdateActiveTourCommandFromResourceAssembler
{
    public static UpdateActiveTourCommand ToCommandFromResource(int activeTourId, UpdateActiveTourResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "UpdateActiveTourResource cannot be null when converting to command.");
        return new UpdateActiveTourCommand(
            activeTourId,
            resource.TourScheduleId, 
            resource.GuideId, 
            resource.CurrentLatitude, 
            resource.CurrentLongitude);
    }
};