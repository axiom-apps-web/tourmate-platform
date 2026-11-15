using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Commands;

public record CreateTourScheduleCommand(
    TourId TourId,
    DateTimeOffset DepartureDateTime,
    int MaxCapacity
    );