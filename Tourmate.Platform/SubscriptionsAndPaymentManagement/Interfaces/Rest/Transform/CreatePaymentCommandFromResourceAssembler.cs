using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

/// <summary>
/// Converts payment creation data into a command.
/// </summary>
public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(
        CreatePaymentResource resource)
    {
        return new CreatePaymentCommand(
            resource.AgencyId,
            new PlanId(resource.PlanId),
            resource.Amount,
            resource.Currency,
            resource.Status,
            resource.RequestedAt
        );
    }
} 