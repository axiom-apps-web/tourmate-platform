namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource representing a subscription payment.
/// </summary>
public record PaymentResource(
    int Id,
    int AgencyId,
    int PlanId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime RequestedAt
); 