using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to create a new agency subscription.
/// </summary>
/// <param name="AgencyId">
/// Identifier of the agency.
/// </param>
/// <param name="PlanId">
/// Identifier of the selected subscription plan.
/// </param>
public record CreateSubscriptionCommand(
    int AgencyId,
    PlanId PlanId
);