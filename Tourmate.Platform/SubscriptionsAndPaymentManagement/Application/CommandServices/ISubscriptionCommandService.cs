using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;

/// <summary>
/// Application service interface for managing subscriptions.
/// </summary>
public interface ISubscriptionCommandService
{
    Task<Subscription> Handle(
        CreateSubscriptionCommand command,
        CancellationToken cancellationToken = default);

    Task<Subscription?> Handle(
        UpdateSubscriptionCommand command,
        CancellationToken cancellationToken = default);

    Task<bool> Handle(
        DeleteSubscriptionCommand command,
        CancellationToken cancellationToken = default);
} 