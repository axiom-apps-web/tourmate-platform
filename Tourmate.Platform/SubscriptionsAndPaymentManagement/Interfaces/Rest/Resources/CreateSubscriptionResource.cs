namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to create an agency subscription.
/// </summary>
public record CreateSubscriptionResource(
    int AgencyId,
    int PlanId,
    string Status,
    DateTime? ActivatedAt
); 