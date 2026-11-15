using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Tourmate.Platform.TourManagement.Domain.Model.Aggregates;
using Tourmate.Platform.TourManagement.Domain.Model.Entities;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public static class ModelBuilderExtensions
{
    public static void ApplyTourManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Tour>().HasKey(t => t.Id);
        builder.Entity<Tour>().Property(t => t.Id)
            .HasConversion(new StronglyTypedIdConverter<TourId, int>(id => id.Value,
                value => new TourId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Tour>().Property(t => t.AgencyId)
            .HasConversion(new StronglyTypedIdConverter<AgencyId, int>(id => id.Value,
                value => new AgencyId(value)))
            .IsRequired();

        builder.Entity<Tour>().Property(t => t.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Entity<Tour>().OwnsOne(t => t.Details,
            d =>
            {
                d.WithOwner().HasForeignKey("Id");
                d.Property(p => p.Title).HasColumnName("Title");
                d.Property(p => p.Description).HasColumnName("Description");
                d.Property(p => p.Duration).HasColumnName("Duration");
                d.Property(p => p.DifficultyLevel).HasColumnName("DifficultyLevel").HasConversion<string>();
            });
        
        builder.Entity<Tour>().OwnsOne(t => t.Price, m =>
        {
            m.WithOwner().HasForeignKey("Id");
            m.Property(x => x.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            m.Property(x => x.Currency).HasColumnName("Currency");
        });
        
        builder.Entity<Tour>().HasMany(t => t.Checkpoints)
            .WithOne()
            .HasForeignKey("TourId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Checkpoint>().HasKey(c => c.Id);
        builder.Entity<Checkpoint>().Property(c => c.Id)
            .HasConversion(new StronglyTypedIdConverter<CheckpointId, int>(id => id.Value, 
                value => new CheckpointId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Checkpoint>().Property(c => c.Name).IsRequired();
        builder.Entity<Checkpoint>().Property(c => c.Order).IsRequired();
        builder.Entity<Checkpoint>().Property(c => c.Description).IsRequired();

        builder.Entity<Checkpoint>().OwnsOne(c => c.Coordinates, co =>
        {
            co.WithOwner().HasForeignKey("Id");
            co.Property(p => p.Xaxis).HasColumnName("Xaxis");
            co.Property(p => p.Yaxis).HasColumnName("Yaxis");
        });
        
        
        builder.Entity<TourSchedule>().HasKey(s => s.Id);
        builder.Entity<TourSchedule>().Property(s => s.Id)
            .HasConversion(new StronglyTypedIdConverter<TourScheduleId, int>(id => id.Value, 
                value => new TourScheduleId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<TourSchedule>().Property(s => s.TourId)
            .HasConversion(new StronglyTypedIdConverter<TourId, int>(id => id.Value, 
                value => new TourId(value)))
            .IsRequired();

        builder.Entity<TourSchedule>().Property(s => s.MaxCapacity).IsRequired();
        builder.Entity<TourSchedule>().Property(s => s.Status).HasConversion<string>().IsRequired();

        builder.Entity<TourSchedule>().OwnsOne(s => s.DepartureDateTime, d =>
        {
            d.WithOwner().HasForeignKey("Id");
            d.Property(p => p.Value).HasColumnName("DepartureDateTime");
        });
        
        builder.Entity<TourSchedule>().HasMany(s => s.Participants)
            .WithOne()
            .HasForeignKey("TourScheduleId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Participant>().HasKey(p => p.Id);
        builder.Entity<Participant>().Property(p => p.Id)
            .HasConversion(new StronglyTypedIdConverter<ParticipantId, int>(id => id.Value, 
                value => new ParticipantId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Entity<Participant>().Property(p => p.UserId)
            .HasConversion(new StronglyTypedIdConverter<UserId, int>(id => id.Value, 
                value => new UserId(value)))
            .IsRequired();
        
        builder.Entity<Participant>().Property(p => p.JoinedAt).IsRequired();

        builder.Entity<Participant>()
            .HasIndex("TourScheduleId", nameof(Participant.UserId))
            .IsUnique();

    }
}