using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts an update plan resource into a command.
/// </summary>
public static class UpdatePlanCommandFromResourceAssembler
{
    public static UpdatePlanCommand ToCommandFromResource(
        int id,
        UpdatePlanResource resource)
    {
        return new UpdatePlanCommand(
            new PlanId(id),
            resource.Name,
            resource.PriceAmount,
            resource.PriceCurrency
        );
    }
} 