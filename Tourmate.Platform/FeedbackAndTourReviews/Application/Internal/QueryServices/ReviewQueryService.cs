using Tourmate.Platform.FeedbackAndTourReviews.Application.QueryServices;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Queries;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Repositories;

namespace Tourmate.Platform.FeedbackAndTourReviews.Application.Internal.QueryServices;

/// <summary>
///     Review query service
/// </summary>
/// <param name="reviewRepository">
///     Review repository
/// </param>
public class ReviewQueryService(IReviewRepository reviewRepository) : IReviewQueryService
{
    /// <inheritdoc />
    public async Task<Review?> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
    {
        return await reviewRepository.FindByIdAsNoTrackingAsync(query.ReviewId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken)
    {
        return await reviewRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Review>> Handle(GetAllReviewsByTourIdQuery query, CancellationToken cancellationToken)
    {
        return await reviewRepository.FindAllByTourIdAsync(query.TourId, cancellationToken);
    }
}