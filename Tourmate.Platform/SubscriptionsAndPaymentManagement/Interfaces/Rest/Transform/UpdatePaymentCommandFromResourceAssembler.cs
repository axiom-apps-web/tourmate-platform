using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts payment update data into a command.
/// </summary>
public static class UpdatePaymentCommandFromResourceAssembler
{
    public static UpdatePaymentCommand ToCommandFromResource(
        int id,
        UpdatePaymentResource resource)
    {
        return new UpdatePaymentCommand(
            new PaymentId(id),
            resource.AgencyId,
            new PlanId(resource.PlanId),
            resource.Amount,
            resource.Currency,
            resource.Status,
            resource.RequestedAt
        );
    }
} 