namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to register a new payment.
/// </summary>
public record CreatePaymentResource(
    int AgencyId,
    int PlanId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime? RequestedAt = null
); 