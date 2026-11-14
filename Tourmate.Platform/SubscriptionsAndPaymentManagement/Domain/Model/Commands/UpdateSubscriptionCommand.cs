using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to update an existing subscription.
/// </summary>
public record UpdateSubscriptionCommand(
    SubscriptionId Id,
    int AgencyId,
    PlanId PlanId,
    string Status,
    DateTime? ActivatedAt
);