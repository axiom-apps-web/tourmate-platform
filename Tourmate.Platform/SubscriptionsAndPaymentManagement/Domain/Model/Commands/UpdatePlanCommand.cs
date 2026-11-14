using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to update an existing subscription plan.
/// </summary>
/// <param name="Id">Identifier of the plan.</param>
/// <param name="Name">Updated name of the plan.</param>
/// <param name="PriceAmount">Updated price.</param>
/// <param name="PriceCurrency">Updated currency code.</param>
public record UpdatePlanCommand(
    PlanId Id,
    string Name,
    decimal PriceAmount,
    string PriceCurrency
);