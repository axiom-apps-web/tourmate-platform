using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tourmate.Platform.FeedbackAndTourReviews.Application.CommandServices;
using Tourmate.Platform.FeedbackAndTourReviews.Application.QueryServices;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Queries;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;
using Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Transform;
using Tourmate.Platform.Resources.Errors;
using Tourmate.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Review endpoints")]
public class ReviewsController(
    IReviewQueryService reviewQueryService,
    IReviewCommandService reviewCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    // ---------- Reviews ----------

    [HttpGet("{reviewId:int}")]
    [EndpointSummary("Get a review by its id")]
    [EndpointDescription("Get a review by its id, including its comments")]
    [ProducesResponseType(typeof(ReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReviewById([FromRoute] int reviewId, CancellationToken cancellationToken)
    {
        var getReviewByIdQuery = new GetReviewByIdQuery(reviewId);
        var review = await reviewQueryService.Handle(getReviewByIdQuery, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromGetReviewByIdResult(
            this,
            review,
            errorLocalizer,
            problemDetailsFactory,
            foundReview => Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(foundReview))
        );
    }

    [HttpGet]
    [EndpointSummary("Get all reviews")]
    [EndpointDescription("Get all reviews. Use the optional tourId parameter to get only the reviews of one tour")]
    [ProducesResponseType(typeof(IEnumerable<ReviewResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllReviews([FromQuery] int? tourId, CancellationToken cancellationToken)
    {
        var reviews = tourId.HasValue
            ? await reviewQueryService.Handle(new GetAllReviewsByTourIdQuery(tourId.Value), cancellationToken)
            : await reviewQueryService.Handle(new GetAllReviewsQuery(), cancellationToken);

        var reviewResources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reviewResources);
    }

    [HttpPost]
    [EndpointSummary("Create a review")]
    [EndpointDescription("Create a review with a rating from 1 to 5 and an optional first comment")]
    [ProducesResponseType(typeof(ReviewResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewResource resource,
        CancellationToken cancellationToken)
    {
        var createReviewCommand = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await reviewCommandService.Handle(createReviewCommand, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            problemDetailsFactory,
            createdReview => CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.Id.Value },
                ReviewResourceFromEntityAssembler.ToResourceFromEntity(createdReview))
        );
    }

    [HttpPut("{reviewId:int}")]
    [EndpointSummary("Update the rating of a review")]
    [EndpointDescription("Update the rating (from 1 to 5) of a review")]
    [ProducesResponseType(typeof(ReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReviewRating([FromRoute] int reviewId,
        [FromBody] UpdateReviewRatingResource resource, CancellationToken cancellationToken)
    {
        var updateReviewRatingCommand =
            UpdateReviewRatingCommandFromResourceAssembler.ToCommandFromResource(reviewId, resource);
        var result = await reviewCommandService.Handle(updateReviewRatingCommand, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            problemDetailsFactory,
            updatedReview => Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(updatedReview))
        );
    }

    [HttpDelete("{reviewId:int}")]
    [EndpointSummary("Delete a review")]
    [EndpointDescription("Delete a review and all its comments")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] int reviewId, CancellationToken cancellationToken)
    {
        var deleteReviewCommand = new DeleteReviewCommand(reviewId);
        var result = await reviewCommandService.Handle(deleteReviewCommand, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            problemDetailsFactory,
            _ => NoContent()
        );
    }

    // ---------- Comments of a review ----------

    [HttpPost("{reviewId:int}/comments")]
    [EndpointSummary("Add a comment to a review")]
    [EndpointDescription("Add a new comment to an existing review")]
    [ProducesResponseType(typeof(CommentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddCommentToReview([FromRoute] int reviewId,
        [FromBody] CommentContentResource resource, CancellationToken cancellationToken)
    {
        var addCommentCommand = CommentCommandFromResourceAssembler.ToAddCommandFromResource(reviewId, resource);
        var result = await reviewCommandService.Handle(addCommentCommand, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            problemDetailsFactory,
            createdComment => CreatedAtAction(nameof(GetReviewById), new { reviewId },
                CommentResourceFromEntityAssembler.ToResourceFromEntity(createdComment))
        );
    }

    [HttpPut("{reviewId:int}/comments/{commentId:int}")]
    [EndpointSummary("Edit a comment of a review")]
    [EndpointDescription("Edit the content of a comment that belongs to a review")]
    [ProducesResponseType(typeof(CommentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateComment([FromRoute] int reviewId, [FromRoute] int commentId,
        [FromBody] CommentContentResource resource, CancellationToken cancellationToken)
    {
        var updateCommentCommand =
            CommentCommandFromResourceAssembler.ToUpdateCommandFromResource(reviewId, commentId, resource);
        var result = await reviewCommandService.Handle(updateCommentCommand, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            problemDetailsFactory,
            updatedComment => Ok(CommentResourceFromEntityAssembler.ToResourceFromEntity(updatedComment))
        );
    }

    [HttpDelete("{reviewId:int}/comments/{commentId:int}")]
    [EndpointSummary("Delete a comment of a review")]
    [EndpointDescription("Delete a comment that belongs to a review")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComment([FromRoute] int reviewId, [FromRoute] int commentId,
        CancellationToken cancellationToken)
    {
        var deleteCommentCommand = new DeleteCommentCommand(reviewId, commentId);
        var result = await reviewCommandService.Handle(deleteCommentCommand, cancellationToken);

        return FeedbackAndTourReviewsActionResultAssembler.ToActionResultFromCommandResult(
            this,
            result,
            problemDetailsFactory,
            _ => NoContent()
        );
    }
}