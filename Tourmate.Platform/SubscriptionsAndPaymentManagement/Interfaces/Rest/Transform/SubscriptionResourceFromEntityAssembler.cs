using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts a subscription entity into a REST resource.
/// </summary>
public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(
        Subscription entity)
    {
        return new SubscriptionResource(
            entity.Id.Value,
            entity.AgencyId,
            entity.PlanId.Value,
            entity.Status,
            entity.ActivatedAt
        );
    }
} 