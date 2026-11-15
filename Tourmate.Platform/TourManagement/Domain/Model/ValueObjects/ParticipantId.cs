using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public readonly record struct ParticipantId(int Value) : IEntityId<int>
{
    public static implicit operator int(ParticipantId id) => id.Value;
    public static implicit operator ParticipantId(int value) => new(value);
    
    public override string ToString() => Value.ToString();
}