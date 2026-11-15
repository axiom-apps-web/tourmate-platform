using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

public readonly record struct AgencyId(int Value) : IEntityId<int>
{
    public static implicit operator int(AgencyId id) => id.Value;
    public static implicit operator AgencyId(int value) => new(value);
    
    public override string ToString() => Value.ToString();
}