using Humanizer;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourMonitoring.Interfaces.Rest.Transform;

public static class ActiveTourResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="ActiveTour" /> domain aggregate to its <see cref="ActiveTourResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="ActiveTour" /> aggregate to convert. Must not be null.
    /// </param>
    /// <returns>
    ///     A <see cref="ActiveTourResource" /> object representing the provided activeTour.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="entity" /> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// 
    /// </exception>
    public static ActiveTourResource ToResourceFromEntity(ActiveTour entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "ActiveTour entity cannot be null when converting to resource.");
        
        return new ActiveTourResource(
            entity.Id,
            entity.TourScheduleId,
            entity.GuideId,
                entity.Status.Humanize(),
                entity.CurrentLatitude,
                entity.CurrentLongitude,
                entity.StartedAt,
                entity.FinishedAt.HasValue ? entity.FinishedAt.Value : null
                );
    }
}