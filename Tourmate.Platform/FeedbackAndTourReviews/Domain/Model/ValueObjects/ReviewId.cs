using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

public readonly record struct ReviewId(int Value) : IEntityId<int>
{
    public static implicit operator int(ReviewId id) => id.Value;
    public static implicit operator ReviewId(int value) => new(value);

    public override string ToString() => Value.ToString();
}