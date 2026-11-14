namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to update an existing agency subscription.
/// </summary>
public record UpdateSubscriptionResource(
    int AgencyId,
    int PlanId,
    string Status,
    DateTime? ActivatedAt
); 