using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;

/// <summary>
/// Application service interface for querying plans.
/// </summary>
public interface IPlanQueryService
{
    Task<IReadOnlyList<Plan>> Handle(
        GetAllPlansQuery query,
        CancellationToken cancellationToken = default);

    Task<Plan?> Handle(
        GetPlanByIdQuery query,
        CancellationToken cancellationToken = default);
} 