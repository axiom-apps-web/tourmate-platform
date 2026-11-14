using Tourmate.Platform.TourMonitoring.Domain.Model.Commands;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Transform;

public static class CreateActiveTourCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateActiveTourResource" /> to a <see cref="CreateActiveTourCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateActiveTourResource" /> containing the data for creating a activeTour. Must not be null.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateActiveTourCommand" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="resource" /> is null.</exception>
    public static CreateActiveTourCommand ToCommandFromResource(CreateActiveTourResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateActiveTourResource cannot be null when converting to command.");
        return new CreateActiveTourCommand(
            resource.TourScheduleId, 
            resource.GuideId, 
            resource.CurrentLatitude, 
            resource.CurrentLongitude);
    }
}