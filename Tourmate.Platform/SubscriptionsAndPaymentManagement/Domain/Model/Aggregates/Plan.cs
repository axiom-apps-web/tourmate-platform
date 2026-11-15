using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;

/// <summary>
/// Represents a subscription plan in Tourmate.
/// </summary>
public class Plan : AggregateRoot
{
    public PlanId Id { get; private set; }
    public string Name { get; private set; }
    public decimal PriceAmount { get; private set; }
    public string PriceCurrency { get; private set; }

    // Constructor required by Entity Framework Core
    private Plan()
    {
        Name = string.Empty;
        PriceCurrency = "USD";
    }

    public Plan(string name, decimal priceAmount,
        string priceCurrency = "USD") : this()
    {
        UpdateDetails(name, priceAmount, priceCurrency);
    }

    /// <summary>
    /// Updates the subscription plan information.
    /// </summary>
    public void UpdateDetails(string name, decimal priceAmount,
        string priceCurrency)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(priceCurrency);

        name = name.Trim();
        priceCurrency = priceCurrency.Trim().ToUpperInvariant();

        if (name.Length > 120)
            throw new ArgumentException(
                "Plan name cannot exceed 120 characters.",
                nameof(name));

        if (priceAmount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(priceAmount),
                "Plan price cannot be negative.");

        if (priceCurrency.Length != 3 ||
            !priceCurrency.All(c => c is >= 'A' and <= 'Z'))
            throw new ArgumentException(
                "Currency must contain three letters.",
                nameof(priceCurrency));

        Name = name;
        PriceAmount = priceAmount;
        PriceCurrency = priceCurrency;
    }
}