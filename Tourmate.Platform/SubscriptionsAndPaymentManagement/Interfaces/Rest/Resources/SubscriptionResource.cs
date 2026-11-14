namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource representing an agency subscription.
/// </summary>
public record SubscriptionResource(
    int Id,
    int AgencyId,
    int PlanId,
    string Status,
    DateTime? ActivatedAt
); 