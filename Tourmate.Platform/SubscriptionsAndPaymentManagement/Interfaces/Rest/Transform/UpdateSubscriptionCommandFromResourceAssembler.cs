using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts subscription update data into a command.
/// </summary>
public static class UpdateSubscriptionCommandFromResourceAssembler
{
    public static UpdateSubscriptionCommand ToCommandFromResource(
        int id,
        UpdateSubscriptionResource resource)
    {
        return new UpdateSubscriptionCommand(
            new SubscriptionId(id),
            resource.AgencyId,
            new PlanId(resource.PlanId),
            resource.Status,
            resource.ActivatedAt
        );
    }
} 