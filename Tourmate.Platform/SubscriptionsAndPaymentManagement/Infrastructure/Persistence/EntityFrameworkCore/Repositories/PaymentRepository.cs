using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Repositories;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Entity Framework Core repository for payment records.
/// </summary>
public class PaymentRepository(AppDbContext context)
    : BaseRepository<Payment>(context), IPaymentRepository
{
    public override async Task<Payment?> FindByIdAsNoTrackingAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Payment>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                payment => payment.Id == new PaymentId(id),
                cancellationToken);
    }
} 