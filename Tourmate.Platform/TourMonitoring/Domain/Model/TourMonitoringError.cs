namespace Tourmate.Platform.TourMonitoring.Domain.Model;

public enum TourMonitoringError
{
    None,
    ActiveTourNotFound,
    DatabaseError,
    InternalServerError,
}