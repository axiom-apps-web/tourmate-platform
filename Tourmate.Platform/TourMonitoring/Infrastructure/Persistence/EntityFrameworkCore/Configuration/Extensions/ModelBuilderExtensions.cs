using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;
using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyTourMonitoringConfigurations(this ModelBuilder builder)
    {
        // Tour Monitoring Context
        
        builder.Entity<ActiveTour>().HasKey(a => a.Id);
        builder.Entity<ActiveTour>().Property(a => a.Id)
            .HasConversion(new StronglyTypedIdConverter<ActiveTourId, int>(id => id.Value, value => new ActiveTourId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<ActiveTour>().Property(a => a.TourScheduleId)
            .HasConversion(new StronglyTypedIdConverter<TourScheduleId, int>(id => id.Value, value => new TourScheduleId(value)))
            .IsRequired();
        builder.Entity<ActiveTour>().Property(a => a.GuideId)
            .HasConversion(new StronglyTypedIdConverter<GuideId, int>(id => id.Value, value => new GuideId(value)))
            .IsRequired();
        builder.Entity<ActiveTour>().Property(a => a.Status).IsRequired().HasConversion<string>().HasMaxLength(20);
        builder.Entity<ActiveTour>().Property(a => a.CurrentLatitude).IsRequired();
        builder.Entity<ActiveTour>().Property(a => a.CurrentLongitude).IsRequired();
        builder.Entity<ActiveTour>().Property(a => a.StartedAt).IsRequired();
        builder.Entity<ActiveTour>().Property(a => a.FinishedAt).IsRequired(false);
    }
}