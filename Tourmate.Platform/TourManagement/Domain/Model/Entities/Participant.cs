using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Entities;

public class Participant
{
    public ParticipantId Id { get; }
    public UserId UserId { get; private set; }
    public DateTimeOffset JoinedAt { get; private set; }

    private Participant()
    {
    }

    public Participant(UserId userId)
    {
        UserId = userId;
        JoinedAt = DateTimeOffset.UtcNow;
    }
}