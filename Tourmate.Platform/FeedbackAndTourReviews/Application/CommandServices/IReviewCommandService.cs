using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Entities;
using Tourmate.Platform.Shared.Application.Model;

namespace Tourmate.Platform.FeedbackAndTourReviews.Application.CommandServices;

public interface IReviewCommandService
{
    
    Task<Result<Review>> Handle(CreateReviewCommand command, CancellationToken cancellationToken);
    
    Task<Result<Review>> Handle(UpdateReviewRatingCommand command, CancellationToken cancellationToken);

    Task<Result<Review>> Handle(DeleteReviewCommand command, CancellationToken cancellationToken);

    Task<Result<Comment>> Handle(AddCommentToReviewCommand command, CancellationToken cancellationToken);

    Task<Result<Comment>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken);

    Task<Result<Review>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken);
}