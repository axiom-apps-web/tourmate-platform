using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

public readonly record struct TourId(int Value) : IEntityId<int>
{
    public static implicit operator int(TourId id) => id.Value;
    public static implicit operator TourId(int value) => new(value);

    public override string ToString() => Value.ToString();
}