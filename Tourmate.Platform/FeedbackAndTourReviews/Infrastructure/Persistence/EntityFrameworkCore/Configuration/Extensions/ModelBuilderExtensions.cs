using Microsoft.EntityFrameworkCore;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Aggregates;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.Entities;
using Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;
using Tourmate.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;

namespace Tourmate.Platform.FeedbackAndTourReviews.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyFeedbackAndTourReviewsConfiguration(this ModelBuilder builder)
    {
        // Feedback And Tour Reviews Context

        // Review aggregate
        builder.Entity<Review>().HasKey(r => r.Id);
        builder.Entity<Review>().Property(r => r.Id)
            .HasConversion(new StronglyTypedIdConverter<ReviewId, int>(id => id.Value, value => new ReviewId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Review>().Property(r => r.UserId)
            .HasConversion(new StronglyTypedIdConverter<UserId, int>(id => id.Value, value => new UserId(value)))
            .IsRequired();
        builder.Entity<Review>().Property(r => r.TourId)
            .HasConversion(new StronglyTypedIdConverter<TourId, int>(id => id.Value, value => new TourId(value)))
            .IsRequired();
        builder.Entity<Review>().Property(r => r.Rating)
            .HasConversion(rating => rating.Value, value => new Rating(value))
            .IsRequired();

        // Comment entity
        builder.Entity<Comment>().HasKey(c => c.Id);
        builder.Entity<Comment>().Property(c => c.Id)
            .HasConversion(new StronglyTypedIdConverter<CommentId, int>(id => id.Value, value => new CommentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Comment>().Property(c => c.ReviewId)
            .HasConversion(new StronglyTypedIdConverter<ReviewId, int>(id => id.Value, value => new ReviewId(value)))
            .IsRequired();
        builder.Entity<Comment>().Property(c => c.Content).IsRequired().HasMaxLength(500);

        // Relationship: one review has many comments
        builder.Entity<Review>()
            .HasMany(r => r.Comments)
            .WithOne()
            .HasForeignKey(c => c.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Review>().Navigation(r => r.Comments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}