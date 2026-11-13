using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Repositories;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Tourmate.Platform.FeedbackAndTourReviews.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Repositories;

/// <summary>
///     Represents the review repository in the Tour Mate Platform.
///     Every query includes the comments of each review.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class ReviewRepository(AppDbContext context) : BaseRepository<Review>(context), IReviewRepository
{
    /// <inheritdoc />
    public override async Task<Review?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Review>()
            .Include(review => review.Comments)
            .FirstOrDefaultAsync(review => review.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<Review?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Review>()
            .Include(review => review.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(review => review.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<Review>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Review>()
            .Include(review => review.Comments)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<Review>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Review>()
            .Include(review => review.Comments)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Review>> FindAllByTourIdAsync(int tourId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Review>()
            .Include(review => review.Comments)
            .AsNoTracking()
            .Where(review => review.TourId == tourId)
            .ToListAsync(cancellationToken);
    }
}