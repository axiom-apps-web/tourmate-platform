using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.Internal.QueryServices;

/// <summary>
/// Query service for subscription plans.
/// </summary>
public class PlanQueryService(IPlanRepository planRepository)
    : IPlanQueryService
{
    public async Task<IReadOnlyList<Plan>> Handle(
        GetAllPlansQuery query,
        CancellationToken cancellationToken = default)
    {
        var plans = await planRepository
            .ListAsNoTrackingAsync(cancellationToken);

        return plans.ToList();
    }

    public async Task<Plan?> Handle(
        GetPlanByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await planRepository
            .FindByIdAsNoTrackingAsync(
                query.Id.Value,
                cancellationToken);
    }
} 