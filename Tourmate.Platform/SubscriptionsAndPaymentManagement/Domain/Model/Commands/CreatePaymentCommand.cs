using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to create a new payment.
/// </summary>
public record CreatePaymentCommand(
    int AgencyId,
    PlanId PlanId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime? RequestedAt = null
);