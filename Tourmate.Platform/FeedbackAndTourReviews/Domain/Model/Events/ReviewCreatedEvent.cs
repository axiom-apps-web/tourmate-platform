using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.Shared.Domain.Model.Events;

namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Events;

public record ReviewCreatedEvent(ReviewId ReviewId, UserId UserId, TourId TourId) : IEvent;