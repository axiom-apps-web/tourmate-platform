using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.Shared.Domain.Model.Entities;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Entities;


public class Comment : IAuditableEntity
{
    public Comment()
    {
        Content = string.Empty;
    }

    public Comment(string content) : this()
    {
        Content = content;
    }

    public CommentId Id { get; }
    public ReviewId ReviewId { get; private set; }
    public string Content { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    
    public void ChangeContent(string content)
    {
        Content = content;
    }
}