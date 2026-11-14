using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;

/// <summary>
/// Represents an agency subscription to a Tourmate plan.
/// </summary>
public class Subscription : AggregateRoot
{
    public SubscriptionId Id { get; private set; }
    public int AgencyId { get; private set; }
    public PlanId PlanId { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public DateTime? ActivatedAt { get; private set; }

    // Constructor required by Entity Framework Core
    private Subscription()
    {
    }

    public Subscription(
        int agencyId,
        PlanId planId,
        string status,
        DateTime? activatedAt = null)
    {
        UpdateDetails(agencyId, planId, status, activatedAt);
    }

    /// <summary>
    /// Updates subscription information.
    /// </summary>
    public void UpdateDetails(
        int agencyId,
        PlanId planId,
        string status,
        DateTime? activatedAt)
    {
        if (agencyId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(agencyId),
                "Agency ID must be positive.");

        if (planId.Value <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(planId),
                "Plan ID must be positive.");

        ArgumentException.ThrowIfNullOrWhiteSpace(status);

        var normalizedStatus = status.Trim().ToUpperInvariant();

        if (normalizedStatus.Length > 30)
            throw new ArgumentException(
                "Subscription status cannot exceed 30 characters.",
                nameof(status));

        AgencyId = agencyId;
        PlanId = planId;
        Status = normalizedStatus;
        ActivatedAt = activatedAt;
    }
}