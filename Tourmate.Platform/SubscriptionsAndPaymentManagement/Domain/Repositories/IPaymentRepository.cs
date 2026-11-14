using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

/// <summary>
/// Repository interface for subscription payments.
/// </summary>
public interface IPaymentRepository : IBaseRepository<Payment>
{
} 