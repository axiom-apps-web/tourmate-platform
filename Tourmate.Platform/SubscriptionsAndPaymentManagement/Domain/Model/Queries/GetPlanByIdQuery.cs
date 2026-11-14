using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;

/// <summary>
/// Query to retrieve a subscription plan by its identifier.
/// </summary>
public record GetPlanByIdQuery(
    PlanId Id
); 