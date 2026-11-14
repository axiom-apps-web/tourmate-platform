using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

/// <summary>
/// Strongly typed identifier for a subscription.
/// </summary>
public readonly record struct SubscriptionId(int Value) : IEntityId<int>
{
    public static implicit operator int(SubscriptionId id) => id.Value;

    public static implicit operator SubscriptionId(int value) => new(value);

    public override string ToString() => Value.ToString();
}