#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration
{
    public class CollectionItemConfiguration : IEntityTypeConfiguration<CollectionItem>
    {
        public void Configure(EntityTypeBuilder<CollectionItem> builder)
        {
            builder.ToTable("collection_items");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("collection_item_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new CollectionItemId(guid));

            builder.HasOne(x => x.CatalogItem);
            builder.Property("CatalogItemId")
                .HasColumnName("catalog_item_id")
                .IsRequired();

            builder.Property("CollectionId")
                .HasColumnName("collection_id")
                .IsRequired();

            builder.HasOne(x => x.PurchasedAt);

            builder.Property(x => x.Condition)
                .HasColumnName("condition")
                .HasMaxLength(15)
                .HasConversion(
                    cond => cond.ToString(),
                    s => EnumHelpers.RequiredValueFor<Condition>(s));

            builder.OwnsOne(x => x.Price, m =>
            {
                m.Property(x => x.Amount)
                    .HasColumnName("price")
                    .IsRequired();

                m.Property(x => x.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

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

            builder.Property(x => x.Notes)
                .HasColumnName("notes")
                .HasMaxLength(150)
                .IsUnicode();
        }
    }
}
