namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource representing a subscription plan.
/// </summary>
public record PlanResource(
    int Id,
    string Name,
    decimal PriceAmount,
    string PriceCurrency
); 