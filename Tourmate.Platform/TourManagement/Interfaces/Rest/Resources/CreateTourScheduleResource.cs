namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

public record CreateTourScheduleResource(
    int TourId,
    DateTimeOffset DepartureDateTime,
    int MaxCapacity
    );