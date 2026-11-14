using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;

/// <summary>
/// Command to delete an existing subscription plan.
/// </summary>
/// <param name="Id">
/// Identifier of the plan to delete.
/// </param>
public record DeletePlanCommand(
    PlanId Id
);