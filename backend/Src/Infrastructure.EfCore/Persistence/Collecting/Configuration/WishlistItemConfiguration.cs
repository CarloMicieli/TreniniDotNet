#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration
{
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("wishlist_items");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("wishlist_item_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new WishlistItemId(guid));

            builder.Property("WishlistId")
                .HasColumnName("wishlist_id")
                .IsRequired();

            builder.HasOne(x => x.CatalogItem);
            builder.Property("CatalogItemId")
                .HasColumnName("catalog_item_id")
                .IsRequired();

            builder.Property(x => x.Priority)
                .HasColumnName("priority")
                .HasMaxLength(10)
                .IsRequired()
                .HasConversion(
                    priority => priority.ToString(),
                    s => EnumHelpers.RequiredValueFor<Priority>(s));

            builder.Property(x => x.AddedDate)
                .HasColumnName("added_date")
                .HasConversion(
                    localDate => localDate.ToDateTimeUnspecified(),
                    d => LocalDate.FromDateTime(d));

            builder.Property(x => x.RemovedDate)
                .HasColumnName("removed_date")
                .HasConversion(
                    localDate => localDate.Value.ToDateTimeUnspecified(),
                    d => LocalDate.FromDateTime(d));

            builder.OwnsOne(x => x.Price, m =>
            {
                m.Property(x => x.Amount)
                    .HasColumnName("price");

                m.Property(x => x.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3);
            });

            builder.Property(x => x.Notes)
                .HasColumnName("notes")
                .HasMaxLength(150)
                .IsUnicode();
        }
    }
}
