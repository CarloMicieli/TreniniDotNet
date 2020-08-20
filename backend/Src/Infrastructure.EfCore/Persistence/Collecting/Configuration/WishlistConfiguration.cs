#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("wishlists");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("wishlist_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new WishlistId(guid));

            builder.HasIndex(b => b.Owner).IsUnique(false);

            builder.Property(x => x.Owner)
                .HasColumnName("owner")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode()
                .HasConversion(
                    o => o.ToString(),
                    s => new Owner(s));

            builder.Property(x => x.Slug)
                .HasColumnName("slug")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50)
                .HasConversion(
                    slug => slug.Value,
                    s => Slug.Of(s));
            builder.HasIndex(x => x.Slug).IsUnique();

            builder.Property(x => x.ListName)
                .HasColumnName("wishlist_name")
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(x => x.Visibility)
                .HasColumnName("visibility")
                .HasMaxLength(15)
                .IsRequired()
                .HasConversion(
                    vis => vis.ToString(),
                    s => EnumHelpers.RequiredValueFor<Visibility>(s));

            builder.OwnsOne(x => x.Budget, m =>
            {
                m.Property(x => x.Amount)
                    .HasColumnName("budget");

                m.Property(x => x.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3);
            });

            builder.HasMany(x => x.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.AddAggregateRootProperties<Wishlist, WishlistId>();
        }
    }
}
