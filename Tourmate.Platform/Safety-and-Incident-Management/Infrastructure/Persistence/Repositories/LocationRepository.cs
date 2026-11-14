using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Repositories;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Infrastructure.Persistence.Repositories;

public class LocationRepository(AppDbContext context) : BaseRepository<LocationRecord>(context), ILocationRepository
{
}