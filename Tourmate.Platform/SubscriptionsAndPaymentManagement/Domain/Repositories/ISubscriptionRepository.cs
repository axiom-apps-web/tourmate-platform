using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

/// <summary>
/// Repository interface for agency subscriptions.
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
} 