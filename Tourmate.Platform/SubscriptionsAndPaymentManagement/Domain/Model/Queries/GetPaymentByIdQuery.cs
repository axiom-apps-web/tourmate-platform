using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;

/// <summary>
/// Query to retrieve a payment by its identifier.
/// </summary>
public record GetPaymentByIdQuery(
    PaymentId Id
); 