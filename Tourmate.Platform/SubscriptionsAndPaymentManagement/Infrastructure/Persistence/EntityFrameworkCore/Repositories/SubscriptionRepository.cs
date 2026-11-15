using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Entity Framework Core repository for agency subscriptions.
/// </summary>
public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public override async Task<Subscription?> FindByIdAsNoTrackingAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Subscription>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                subscription => subscription.Id == new SubscriptionId(id),
                cancellationToken);
    }
} 