using Tourmate.Platform.Shared.Domain.Model.Errors;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Errors;

public class TourMonitoringErrors
{
    public static readonly Error ActiveTourNotFound =
        Error.NotFound("TourMonitoring.ActiveTourNotFound", "The specified active tour was not found.");
}