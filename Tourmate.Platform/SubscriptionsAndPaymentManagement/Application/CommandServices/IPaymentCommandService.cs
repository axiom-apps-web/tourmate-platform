using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;

/// <summary>
/// Application service interface for managing payments.
/// </summary>
public interface IPaymentCommandService
{
    Task<Payment> Handle(
        CreatePaymentCommand command,
        CancellationToken cancellationToken = default);

    Task<Payment?> Handle(
        UpdatePaymentCommand command,
        CancellationToken cancellationToken = default);

    Task<bool> Handle(
        DeletePaymentCommand command,
        CancellationToken cancellationToken = default);
} 