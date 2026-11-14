
using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Aggregates;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Entity Framework Core configuration for subscriptions and payments.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionsAndPaymentManagementConfiguration(
        this ModelBuilder builder)
    {
        // PLAN CONFIGURATION

        builder.Entity<Plan>(entity =>
        {
            entity.ToTable("subscription_plans");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasConversion(
                    new StronglyTypedIdConverter<PlanId, int>(
                        id => id.Value,
                        value => new PlanId(value)))
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(120);

            entity.Property(p => p.PriceAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(p => p.PriceCurrency)
                .IsRequired()
                .HasMaxLength(3);
        });

        // SUBSCRIPTION CONFIGURATION

        builder.Entity<Subscription>(entity =>
        {
            entity.ToTable("agency_subscriptions");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasConversion(
                    new StronglyTypedIdConverter<SubscriptionId, int>(
                        id => id.Value,
                        value => new SubscriptionId(value)))
                .ValueGeneratedOnAdd();

            entity.Property(s => s.AgencyId)
                .IsRequired();

            entity.Property(s => s.PlanId)
                .HasConversion(
                    new StronglyTypedIdConverter<PlanId, int>(
                        id => id.Value,
                        value => new PlanId(value)))
                .IsRequired();

            entity.Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(s => s.ActivatedAt);

            entity.HasOne<Plan>()
                .WithMany()
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // PAYMENT CONFIGURATION

        builder.Entity<Payment>(entity =>
        {
            entity.ToTable("subscription_payments");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasConversion(
                    new StronglyTypedIdConverter<PaymentId, int>(
                        id => id.Value,
                        value => new PaymentId(value)))
                .ValueGeneratedOnAdd();

            entity.Property(p => p.AgencyId)
                .IsRequired();

            entity.Property(p => p.PlanId)
                .HasConversion(
                    new StronglyTypedIdConverter<PlanId, int>(
                        id => id.Value,
                        value => new PlanId(value)))
                .IsRequired();

            entity.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(3);

            entity.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(p => p.RequestedAt)
                .IsRequired();

            entity.HasOne<Plan>()
                .WithMany()
                .HasForeignKey(p => p.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
 