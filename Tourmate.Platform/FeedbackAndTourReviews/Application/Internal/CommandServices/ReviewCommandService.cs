using Tourmate.Platform.FeedbackAndTourReviews.Application.CommandServices;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Entities;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Errors;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Repositories;
using Tourmate.Platform.Shared.Application.Model;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.FeedbackAndTourReviews.Application.Internal.CommandServices;

public class ReviewCommandService(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
    : IReviewCommandService
{
    /// <inheritdoc />
    public async Task<Result<Review>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        if (!Rating.IsValid(command.Rating))
            return Result<Review>.Failure(FeedbackAndTourReviewsErrors.InvalidRating,
                FeedbackAndTourReviewsError.InvalidRating);

        var review = new Review(command);
        await reviewRepository.AddAsync(review, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Review>.Success(review);
    }

    /// <inheritdoc />
    public async Task<Result<Review>> Handle(UpdateReviewRatingCommand command, CancellationToken cancellationToken)
    {
        if (!Rating.IsValid(command.Rating))
            return Result<Review>.Failure(FeedbackAndTourReviewsErrors.InvalidRating,
                FeedbackAndTourReviewsError.InvalidRating);

        var review = await reviewRepository.FindByIdAsync(command.ReviewId, cancellationToken);
        if (review is null)
            return Result<Review>.Failure(FeedbackAndTourReviewsErrors.ReviewNotFound,
                FeedbackAndTourReviewsError.ReviewNotFound);

        review.UpdateRating(command.Rating);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Review>.Success(review);
    }

    /// <inheritdoc />
    public async Task<Result<Review>> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.FindByIdAsync(command.ReviewId, cancellationToken);
        if (review is null)
            return Result<Review>.Failure(FeedbackAndTourReviewsErrors.ReviewNotFound,
                FeedbackAndTourReviewsError.ReviewNotFound);

        reviewRepository.Remove(review);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Review>.Success(review);
    }

    /// <inheritdoc />
    public async Task<Result<Comment>> Handle(AddCommentToReviewCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Content))
            return Result<Comment>.Failure(FeedbackAndTourReviewsErrors.InvalidCommentContent,
                FeedbackAndTourReviewsError.InvalidCommentContent);

        var review = await reviewRepository.FindByIdAsync(command.ReviewId, cancellationToken);
        if (review is null)
            return Result<Comment>.Failure(FeedbackAndTourReviewsErrors.ReviewNotFound,
                FeedbackAndTourReviewsError.ReviewNotFound);

        var comment = review.AddComment(command.Content);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Comment>.Success(comment);
    }

    /// <inheritdoc />
    public async Task<Result<Comment>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Content))
            return Result<Comment>.Failure(FeedbackAndTourReviewsErrors.InvalidCommentContent,
                FeedbackAndTourReviewsError.InvalidCommentContent);

        var review = await reviewRepository.FindByIdAsync(command.ReviewId, cancellationToken);
        if (review is null)
            return Result<Comment>.Failure(FeedbackAndTourReviewsErrors.ReviewNotFound,
                FeedbackAndTourReviewsError.ReviewNotFound);

        if (!review.EditComment(command.CommentId, command.Content))
            return Result<Comment>.Failure(FeedbackAndTourReviewsErrors.CommentNotFound,
                FeedbackAndTourReviewsError.CommentNotFound);

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Comment>.Success(review.FindCommentById(command.CommentId)!);
    }

    /// <inheritdoc />
    public async Task<Result<Review>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.FindByIdAsync(command.ReviewId, cancellationToken);
        if (review is null)
            return Result<Review>.Failure(FeedbackAndTourReviewsErrors.ReviewNotFound,
                FeedbackAndTourReviewsError.ReviewNotFound);

        if (!review.DeleteComment(command.CommentId))
            return Result<Review>.Failure(FeedbackAndTourReviewsErrors.CommentNotFound,
                FeedbackAndTourReviewsError.CommentNotFound);

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Review>.Success(review);
    }
}