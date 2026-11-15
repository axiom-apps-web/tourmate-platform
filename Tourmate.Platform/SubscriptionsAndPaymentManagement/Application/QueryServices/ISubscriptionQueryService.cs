using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;

/// <summary>
/// Application service interface for querying subscriptions.
/// </summary>
public interface ISubscriptionQueryService
{
    Task<IReadOnlyList<Subscription>> Handle(
        GetAllSubscriptionsQuery query,
        CancellationToken cancellationToken = default);

    Task<Subscription?> Handle(
        GetSubscriptionByIdQuery query,
        CancellationToken cancellationToken = default);
}
