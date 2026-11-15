using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts a resource for creating a plan into a command. 
/// </summary>
public static class CreatePlanCommandFromResourceAssembler
{
    public static CreatePlanCommand ToCommandFromResource(
        CreatePlanResource resource)
    {
        return new CreatePlanCommand(
            resource.Name,
            resource.PriceAmount,
            resource.PriceCurrency
        );
    }
} 