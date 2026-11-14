using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;
using Tourmate.Platform.Shared.Domain.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Application.Internal.CommandServices;

public class IncidentCommandService(
    IIncidentRepository incidentRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<Incident> HandleCreateAsync(
        string uuid, int activeTourId, int reportedByUserId, string description,
        double latitude, double longitude, DateTimeOffset reportedAt,
        string status, string severity, string resolutionNote, string reporterRole,
        CancellationToken cancellationToken)
    {
        var incident = new Incident(
            uuid, activeTourId, reportedByUserId, description,
            latitude, longitude, reportedAt, status, severity,
            resolutionNote, reporterRole
        );

        await incidentRepository.AddAsync(incident, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return incident;
    }

    public async Task<Incident?> HandleUpdateAsync(
        int id, string description, double latitude, double longitude,
        string status, string severity, string resolutionNote, string reporterRole,
        CancellationToken cancellationToken)
    {
        var incident = await incidentRepository.FindByIdAsync(id, cancellationToken);
        if (incident == null) return null;

        incident.UpdateDetails(description, latitude, longitude, status, severity, resolutionNote, reporterRole);
        incidentRepository.Update(incident);
        await unitOfWork.CompleteAsync(cancellationToken);
        return incident;
    }

    public async Task<bool> HandleDeleteAsync(int id, CancellationToken cancellationToken)
    {
        var incident = await incidentRepository.FindByIdAsync(id, cancellationToken);
        if (incident == null) return false;

        incidentRepository.Remove(incident);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}