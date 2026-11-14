using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

/// <summary>
/// Strongly typed identifier for a subscription plan.
/// </summary>
public readonly record struct PlanId(int Value) : IEntityId<int>
{
    public static implicit operator int(PlanId id) => id.Value;

    public static implicit operator PlanId(int value) => new(value);

    public override string ToString() => Value.ToString();
} 