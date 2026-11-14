using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;

/// <summary>
/// Application service interface for querying payments.
/// </summary>
public interface IPaymentQueryService
{
    Task<IReadOnlyList<Payment>> Handle(
        GetAllPaymentsQuery query,
        CancellationToken cancellationToken = default);

    Task<Payment?> Handle(
        GetPaymentByIdQuery query,
        CancellationToken cancellationToken = default);
} 