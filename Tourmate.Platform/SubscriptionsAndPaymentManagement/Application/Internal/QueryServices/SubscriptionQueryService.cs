using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.Internal.QueryServices;

/// <summary>
/// Query service for agency subscriptions.
/// </summary>
public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository)
    : ISubscriptionQueryService
{
    public async Task<IReadOnlyList<Subscription>> Handle(
        GetAllSubscriptionsQuery query,
        CancellationToken cancellationToken = default)
    {
        var subscriptions = await subscriptionRepository
            .ListAsNoTrackingAsync(cancellationToken);

        return subscriptions.ToList();
    }

    public async Task<Subscription?> Handle(
        GetSubscriptionByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await subscriptionRepository
            .FindByIdAsNoTrackingAsync(
                query.Id.Value,
                cancellationToken);
    }
}
