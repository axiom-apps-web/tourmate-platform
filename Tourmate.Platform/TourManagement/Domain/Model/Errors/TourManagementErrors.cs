using Tourmate.Platform.Shared.Domain.Model.Errors;

namespace Tourmate.Platform.TourManagement.Domain.Model.Errors;

public static class TourManagementErrors
{
    public static readonly Error TourNotFound =
        Error.NotFound("TourMonitoring.TourNotFound", "The specified tour was not found");
    
    public static readonly Error TourScheduleNotFound =
        Error.NotFound("TourManagement.TourScheduleNotFound", "The specified tour schedule was not found");
}