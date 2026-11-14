
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.Internal.CommandServices;

/// <summary>
/// Handles commands related to subscription payments.
/// </summary>
public class PaymentCommandService(
    IPaymentRepository paymentRepository,
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork) : IPaymentCommandService
{
    public async Task<Payment> Handle(
        CreatePaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = await planRepository.FindByIdAsync(
            command.PlanId.Value,
            cancellationToken);

        if (plan is null)
            throw new ArgumentException(
                "The selected plan does not exist.");

        var payment = new Payment(
            command.AgencyId,
            command.PlanId,
            command.Amount,
            command.Currency,
            command.Status,
            command.RequestedAt);

        await paymentRepository.AddAsync(
            payment,
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);

        return payment;
    }

    public async Task<Payment?> Handle(
        UpdatePaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        var payment = await paymentRepository.FindByIdAsync(
            command.Id.Value,
            cancellationToken);

        if (payment is null)
            return null;

        var plan = await planRepository.FindByIdAsync(
            command.PlanId.Value,
            cancellationToken);

        if (plan is null)
            throw new ArgumentException(
                "The selected plan does not exist.");

        payment.UpdateDetails(
            command.AgencyId,
            command.PlanId,
            command.Amount,
            command.Currency,
            command.Status,
            command.RequestedAt);

        await unitOfWork.CompleteAsync(cancellationToken);

        return payment;
    }

    public async Task<bool> Handle(
        DeletePaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        var payment = await paymentRepository.FindByIdAsync(
            command.Id.Value,
            cancellationToken);

        if (payment is null)
            return false;

        paymentRepository.Remove(payment);

        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}
