namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to create a new subscription plan.
/// </summary>
/// <param name="Name">Name of the plan.</param>
/// <param name="PriceAmount">Price of the plan.</param>
/// <param name="PriceCurrency">Currency code.</param>
public record CreatePlanCommand(
    string Name,
    decimal PriceAmount,
    string PriceCurrency
);