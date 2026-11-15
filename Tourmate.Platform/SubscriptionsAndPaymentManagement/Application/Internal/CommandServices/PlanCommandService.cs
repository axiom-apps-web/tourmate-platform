
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.Internal.CommandServices;

/// <summary>
/// Handles commands related to subscription plans.
/// </summary>
public class PlanCommandService(
    IPlanRepository planRepository,
    ISubscriptionRepository subscriptionRepository,
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork) : IPlanCommandService
{
    public async Task<Plan> Handle(
        CreatePlanCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = new Plan(
            command.Name,
            command.PriceAmount,
            command.PriceCurrency);

        await planRepository.AddAsync(plan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return plan;
    }

    public async Task<Plan?> Handle(
        UpdatePlanCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = await planRepository.FindByIdAsync(
            command.Id.Value, cancellationToken);

        if (plan is null)
            return null;

        plan.UpdateDetails(
            command.Name,
            command.PriceAmount,
            command.PriceCurrency);

        await unitOfWork.CompleteAsync(cancellationToken);

        return plan;
    }

    public async Task<bool> Handle(
        DeletePlanCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = await planRepository.FindByIdAsync(
            command.Id.Value, cancellationToken);

        if (plan is null)
            return false;

        var subscriptions = await subscriptionRepository
            .ListAsNoTrackingAsync(cancellationToken);

        var payments = await paymentRepository
            .ListAsNoTrackingAsync(cancellationToken);

        if (subscriptions.Any(s => s.PlanId.Value == command.Id.Value)
            || payments.Any(p => p.PlanId.Value == command.Id.Value))
        {
            throw new InvalidOperationException(
                "Cannot delete a plan with subscriptions or payments.");
        }

        planRepository.Remove(plan);

        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}
