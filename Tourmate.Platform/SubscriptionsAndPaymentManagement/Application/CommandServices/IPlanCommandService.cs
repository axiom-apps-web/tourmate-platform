using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;

/// <summary>
/// Application service interface for managing plans.
/// </summary>
public interface IPlanCommandService
{
    Task<Plan> Handle(
        CreatePlanCommand command,
        CancellationToken cancellationToken = default);

    Task<Plan?> Handle(
        UpdatePlanCommand command,
        CancellationToken cancellationToken = default);

    Task<bool> Handle(
        DeletePlanCommand command,
        CancellationToken cancellationToken = default);
} 