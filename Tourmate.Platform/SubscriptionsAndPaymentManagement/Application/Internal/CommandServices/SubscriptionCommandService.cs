
using Tourmate.Platform.Shared.Domain.Repositories;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.Internal.CommandServices;

/// <summary>
/// Handles commands related to agency subscriptions.
/// </summary>
public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    public async Task<Subscription> Handle(
        CreateSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var plan = await planRepository.FindByIdAsync(
            command.PlanId.Value, cancellationToken);

        if (plan is null)
            throw new ArgumentException(
                "The selected plan does not exist.");

        var subscription = new Subscription(
            command.AgencyId,
            command.PlanId,
            command.Status,
            command.ActivatedAt);

        await subscriptionRepository.AddAsync(
            subscription, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);

        return subscription;
    }

    public async Task<Subscription?> Handle(
        UpdateSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(
            command.Id.Value, cancellationToken);

        if (subscription is null)
            return null;

        var plan = await planRepository.FindByIdAsync(
            command.PlanId.Value, cancellationToken);

        if (plan is null)
            throw new ArgumentException(
                "The selected plan does not exist.");

        subscription.UpdateDetails(
            command.AgencyId,
            command.PlanId,
            command.Status,
            command.ActivatedAt);

        await unitOfWork.CompleteAsync(cancellationToken);

        return subscription;
    }

    public async Task<bool> Handle(
        DeleteSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(
            command.Id.Value, cancellationToken);

        if (subscription is null)
            return false;

        subscriptionRepository.Remove(subscription);

        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}
 