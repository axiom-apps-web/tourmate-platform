using Tourmate.Platform.Shared.Domain.Model.Entities;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;

public partial class Review : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}