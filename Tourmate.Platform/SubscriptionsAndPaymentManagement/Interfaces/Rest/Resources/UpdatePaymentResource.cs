namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to update an existing payment.
/// </summary>
public record UpdatePaymentResource(
    int AgencyId,
    int PlanId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime RequestedAt
);
