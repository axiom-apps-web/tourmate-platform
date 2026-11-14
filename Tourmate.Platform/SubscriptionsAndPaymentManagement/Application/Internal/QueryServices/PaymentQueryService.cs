using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.Internal.QueryServices;

/// <summary>
/// Query service for subscription payments.
/// </summary>
public class PaymentQueryService(IPaymentRepository paymentRepository)
    : IPaymentQueryService
{
    public async Task<IReadOnlyList<Payment>> Handle(
        GetAllPaymentsQuery query,
        CancellationToken cancellationToken = default)
    {
        var payments = await paymentRepository
            .ListAsNoTrackingAsync(cancellationToken);

        return payments.ToList();
    }

    public async Task<Payment?> Handle(
        GetPaymentByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await paymentRepository
            .FindByIdAsNoTrackingAsync(
                query.Id.Value,
                cancellationToken);
    }
} 