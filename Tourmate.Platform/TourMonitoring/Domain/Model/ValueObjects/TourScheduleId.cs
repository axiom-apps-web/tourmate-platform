using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

public  readonly record struct TourScheduleId(int Value): IEntityId<int>
    

{
    public static implicit operator int(TourScheduleId id) => id.Value;
    public static implicit operator TourScheduleId(int value) => new(value);

    public override string ToString() => Value.ToString();
}