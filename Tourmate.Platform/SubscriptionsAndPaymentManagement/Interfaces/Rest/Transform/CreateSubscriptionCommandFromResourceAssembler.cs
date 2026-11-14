using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts subscription creation data into a command.
/// </summary>
public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(
        CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(
            resource.AgencyId,
            new PlanId(resource.PlanId),
            resource.Status,
            resource.ActivatedAt
        );
    }
} 