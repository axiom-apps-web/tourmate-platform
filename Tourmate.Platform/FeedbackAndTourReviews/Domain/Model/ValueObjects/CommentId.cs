using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

public readonly record struct CommentId(int Value) : IEntityId<int>
{
    public static implicit operator int(CommentId id) => id.Value;
    public static implicit operator CommentId(int value) => new(value);

    public override string ToString() => Value.ToString();
}