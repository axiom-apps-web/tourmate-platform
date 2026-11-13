namespace Tourmate.Platform.FeedbackAndTourReviews.Interfaces.Rest.Resources;

/// <summary>
///     Resource used to create or edit a comment. It only carries the text of the comment.
/// </summary>
public record CommentContentResource(string Content);