namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

public record ParticipantResource(
    int ParticipantId,
    int UserId,
    DateTime JoinedAt
    );