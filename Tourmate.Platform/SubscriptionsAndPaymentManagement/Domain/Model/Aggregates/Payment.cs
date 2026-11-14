
using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;

/// <summary>
/// Represents a subscription payment in Tourmate.
/// </summary>
public class Payment : AggregateRoot
{
    public PaymentId Id { get; private set; }
    public int AgencyId { get; private set; }
    public PlanId PlanId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public DateTime RequestedAt { get; private set; }

    // Constructor required by Entity Framework Core
    private Payment()
    {
    }

    public Payment(
        int agencyId,
        PlanId planId,
        decimal amount,
        string currency,
        string status,
        DateTime? requestedAt = null)
    {
        if (agencyId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(agencyId),
                "Agency ID must be positive.");

        if (planId.Value <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(planId),
                "Plan ID must be positive.");

        if (amount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Payment amount cannot be negative.");

        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(status);

        currency = currency.Trim().ToUpperInvariant();
        status = status.Trim();

        if (currency.Length != 3 ||
            !currency.All(c => c is >= 'A' and <= 'Z'))
            throw new ArgumentException(
                "Currency must contain three letters.",
                nameof(currency));

        if (status.Length > 30)
            throw new ArgumentException(
                "Payment status cannot exceed 30 characters.",
                nameof(status));

        AgencyId = agencyId;
        PlanId = planId;
        Amount = amount;
        Currency = currency;
        Status = status;
        RequestedAt = requestedAt ?? DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the current payment status.
    /// </summary>
    public void UpdateStatus(string status)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(status);

        status = status.Trim();

        if (status.Length > 30)
            throw new ArgumentException(
                "Payment status cannot exceed 30 characters.",
                nameof(status));

        Status = status;
    }
}
