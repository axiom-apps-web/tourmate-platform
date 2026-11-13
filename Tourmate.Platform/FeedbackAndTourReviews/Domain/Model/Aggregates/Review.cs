using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Commands;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Entities;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Events;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;

/// <summary>
///     Review aggregate root. Represents the rating and comments a traveler leaves about a tour.
/// </summary>
public partial class Review : AggregateRoot
{
    private readonly List<Comment> _comments = [];

    public Review()
    {
        Rating = new Rating(Rating.MinValue);
    }

    public Review(UserId userId, TourId tourId, Rating rating) : this()
    {
        UserId = userId;
        TourId = tourId;
        Rating = rating;
    }

    public Review(CreateReviewCommand command) : this(command.UserId, command.TourId, new Rating(command.Rating))
    {
        if (!string.IsNullOrWhiteSpace(command.Comment)) AddComment(command.Comment);
        AddDomainEvent(new ReviewCreatedEvent(Id, UserId, TourId));
    }

    public ReviewId Id { get; }
    public UserId UserId { get; private set; }
    public TourId TourId { get; private set; }
    public Rating Rating { get; private set; }
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

   
    public void UpdateRating(int rating)
    {
        Rating = new Rating(rating);
    }

   
    public Comment AddComment(string content)
    {
        var comment = new Comment(content);
        _comments.Add(comment);
        return comment;
    }

  
    public Comment? FindCommentById(int commentId)
    {
        return _comments.FirstOrDefault(comment => comment.Id.Value == commentId);
    }

    
    public bool EditComment(int commentId, string content)
    {
        var comment = FindCommentById(commentId);
        if (comment is null) return false;
        comment.ChangeContent(content);
        return true;
    }

 
    public bool DeleteComment(int commentId)
    {
        var comment = FindCommentById(commentId);
        if (comment is null) return false;
        _comments.Remove(comment);
        return true;
    }
}