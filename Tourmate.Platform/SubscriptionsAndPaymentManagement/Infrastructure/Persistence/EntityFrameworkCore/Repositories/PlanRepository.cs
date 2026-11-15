using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Entity Framework Core repository for subscription plans.
/// </summary>
public class PlanRepository(AppDbContext context)
    : BaseRepository<Plan>(context), IPlanRepository
{
    public override async Task<Plan?> FindByIdAsNoTrackingAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Plan>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                plan => plan.Id == new PlanId(id),
                cancellationToken);
    }
}