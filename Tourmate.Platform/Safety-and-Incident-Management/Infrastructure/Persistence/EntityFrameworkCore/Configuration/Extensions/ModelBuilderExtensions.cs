using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.SafetyAndIncidentManagement.Domain.Model.Aggregates;

namespace Tourmate.Platform.SafetyAndIncidentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySafetyAndIncidentManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Incident>().HasKey(i => i.Id);
        builder.Entity<Incident>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Incident>().Property(i => i.Uuid).IsRequired();
        builder.Entity<Incident>().Property(i => i.ActiveTourId).IsRequired();
        builder.Entity<Incident>().Property(i => i.ReportedByUserId).IsRequired();
        builder.Entity<Incident>().Property(i => i.Description).IsRequired();
        builder.Entity<Incident>().Property(i => i.Latitude).IsRequired();
        builder.Entity<Incident>().Property(i => i.Longitude).IsRequired();
        builder.Entity<Incident>().Property(i => i.ReportedAt).IsRequired();
        builder.Entity<Incident>().Property(i => i.Status).IsRequired();
        builder.Entity<Incident>().Property(i => i.Severity).IsRequired();
        builder.Entity<Incident>().Property(i => i.ResolutionNote).IsRequired(false);
        builder.Entity<Incident>().Property(i => i.ReporterRole).IsRequired();

        builder.Entity<LocationRecord>().HasKey(l => l.Id);
        builder.Entity<LocationRecord>().Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<LocationRecord>().Property(l => l.IncidentId).IsRequired(false);
        builder.Entity<LocationRecord>().Property(l => l.ExpeditionId).IsRequired();
        builder.Entity<LocationRecord>().Property(l => l.TouristId).IsRequired();
        builder.Entity<LocationRecord>().Property(l => l.Latitude).IsRequired();
        builder.Entity<LocationRecord>().Property(l => l.Longitude).IsRequired();
        builder.Entity<LocationRecord>().Property(l => l.RecordedAt).IsRequired();
        builder.Entity<LocationRecord>().Property(l => l.ExpectedLatitude).IsRequired(false);
        builder.Entity<LocationRecord>().Property(l => l.ExpectedLongitude).IsRequired(false);
        builder.Entity<LocationRecord>().Property(l => l.Alerts).IsRequired(false);
        builder.Entity<LocationRecord>().Property(l => l.Telemetry).IsRequired(false);
        builder.Entity<LocationRecord>().Property(l => l.Wearables).IsRequired(false);

        builder.Entity<LocationRecord>()
            .HasOne<Incident>()
            .WithMany()
            .HasForeignKey(l => l.IncidentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

