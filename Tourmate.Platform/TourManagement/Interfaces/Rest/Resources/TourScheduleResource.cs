namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

public record TourScheduleResource(
    int Id,
    int TourId,
    DateTimeOffset DepartureDateTime,
    int MaxCapacity,
    List<ParticipantResource> Participants
    );