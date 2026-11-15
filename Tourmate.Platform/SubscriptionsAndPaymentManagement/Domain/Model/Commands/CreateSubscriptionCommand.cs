using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to create a new agency subscription.
/// </summary>
public record CreateSubscriptionCommand(
    int AgencyId,
    PlanId PlanId,
    string Status = "ACTIVE",
    DateTime? ActivatedAt = null
);