using Tourmate.Platform.Shared.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

/// <summary>
/// Strongly typed identifier for a payment.
/// </summary>
public readonly record struct PaymentId(int Value) : IEntityId<int>
{
    public static implicit operator int(PaymentId id) => id.Value;

    public static implicit operator PaymentId(int value) => new(value);

    public override string ToString() => Value.ToString();
}