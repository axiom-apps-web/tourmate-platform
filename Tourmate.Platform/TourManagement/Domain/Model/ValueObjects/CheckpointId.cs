using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public readonly record struct CheckpointId(int Value) : IEntityId<int>
{
    public static implicit operator int(CheckpointId id) => id.Value;
    public static implicit operator CheckpointId(int value) => new(value);
    
    public override string ToString() => Value.ToString();
}