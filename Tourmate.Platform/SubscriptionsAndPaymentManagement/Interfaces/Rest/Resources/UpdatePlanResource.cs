namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to update an existing subscription plan.
/// </summary>
public record UpdatePlanResource(
    string Name,
    decimal PriceAmount,
    string PriceCurrency
); 