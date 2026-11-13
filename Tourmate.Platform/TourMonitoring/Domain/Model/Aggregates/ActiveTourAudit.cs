using Tourmate.Platform.Shared.Domain.Model.Entities;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;

public partial class ActiveTour : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}