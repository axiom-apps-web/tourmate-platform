using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to delete an existing subscription.
/// </summary>
public record DeleteSubscriptionCommand(
    SubscriptionId Id
);

