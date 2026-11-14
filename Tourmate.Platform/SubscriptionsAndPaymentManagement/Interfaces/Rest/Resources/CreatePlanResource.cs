namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to create a subscription plan.
/// </summary>
public record CreatePlanResource(
    string Name,
    decimal PriceAmount,
    string PriceCurrency
); 