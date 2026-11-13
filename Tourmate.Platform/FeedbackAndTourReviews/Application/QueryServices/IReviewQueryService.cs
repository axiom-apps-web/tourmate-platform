using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Queries;

namespace Tourmate.Platform.FeedbackAndTourReviews.Application.QueryServices;

public interface IReviewQueryService
{
 
    Task<Review?> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<Review>> Handle(GetAllReviewsByTourIdQuery query, CancellationToken cancellationToken);
}