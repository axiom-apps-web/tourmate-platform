using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to update an existing payment.
/// </summary>
public record UpdatePaymentCommand(
    PaymentId Id,
    int AgencyId,
    PlanId PlanId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime RequestedAt
);
