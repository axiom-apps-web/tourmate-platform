using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts a payment entity into a REST resource.
/// </summary>
public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(
        Payment entity)
    {
        return new PaymentResource(
            entity.Id.Value,
            entity.AgencyId,
            entity.PlanId.Value,
            entity.Amount,
            entity.Currency,
            entity.Status,
            entity.RequestedAt
        );
    }
} 