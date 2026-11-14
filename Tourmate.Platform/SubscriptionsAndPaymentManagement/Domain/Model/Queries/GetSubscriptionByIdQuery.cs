using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;

/// <summary>
/// Query to retrieve a subscription by its identifier.
/// </summary>
public record GetSubscriptionByIdQuery(
    SubscriptionId Id
); 