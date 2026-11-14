using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

/// <summary>
/// Repository interface for subscription plans.
/// </summary>
public interface IPlanRepository : IBaseRepository<Plan>
{
} 