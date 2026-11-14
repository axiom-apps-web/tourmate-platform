using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

public  readonly record struct ActiveTourId(int Value): IEntityId<int>
    

{
    public static implicit operator int(ActiveTourId id) => id.Value;
    public static implicit operator ActiveTourId(int value) => new(value);

    public override string ToString() => Value.ToString();
}