using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts a plan entity into a REST resource.
/// </summary>
public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan entity)
    {
        return new PlanResource(
            entity.Id.Value,
            entity.Name,
            entity.PriceAmount,
            entity.PriceCurrency
        );
    }
} 