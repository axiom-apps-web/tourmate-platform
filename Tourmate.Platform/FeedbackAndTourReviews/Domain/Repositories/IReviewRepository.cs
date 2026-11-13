using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    /// <summary>
    ///     Lists all the reviews of a tour, including their comments.
    /// </summary>
    Task<IEnumerable<Review>> FindAllByTourIdAsync(int tourId, CancellationToken cancellationToken = default);
}