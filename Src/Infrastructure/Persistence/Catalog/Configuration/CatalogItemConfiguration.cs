#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public sealed class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("catalog_items");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Slug).IsUnique();
            // builder.HasIndex(x => new {x.ItemNumber, x.Brand}).IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("catalog_item_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new CatalogItemId(guid));

            builder.Property(x => x.ItemNumber)
                .HasColumnName("item_number")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(10)
                .HasConversion(
                    itemNumber => itemNumber.Value,
                    s => new ItemNumber(s));

            builder.Property(x => x.Slug)
                .HasColumnName("slug")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(40)
                .HasConversion(
                    slug => slug.Value,
                    s => Slug.Of(s));

            builder.HasOne(x => x.Brand);
            builder.HasOne(x => x.Scale);

            builder.HasMany(x => x.RollingStocks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<BrandId>("BrandId")
                .HasColumnName("brand_id")
                .IsRequired()
                .HasConversion(
                    id => (Guid)id,
                    guid => new BrandId(guid));

            builder.Property<ScaleId>("ScaleId")
                .HasColumnName("scale_id")
                .IsRequired()
                .HasConversion(
                    id => id.ToGuid(),
                    guid => new ScaleId(guid));

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder.Property(x => x.PrototypeDescription)
                .HasColumnName("prototype_description")
                .IsUnicode()
                .HasMaxLength(2500);

            builder.Property(x => x.ModelDescription)
                .HasColumnName("model_description")
                .IsUnicode()
                .HasMaxLength(2500);

            builder.Property(x => x.PowerMethod)
                .HasColumnName("power_method")
                .HasMaxLength(2)
                .IsRequired()
                .HasConversion(
                    pm => pm.ToString(),
                    s => EnumHelpers.RequiredValueFor<PowerMethod>(s));

            builder.Property(x => x.DeliveryDate)
                .HasColumnName("delivery_date")
                .HasMaxLength(10)
                .HasConversion(
                    dd => dd.ToString(),
                    s => DeliveryDate.Parse(s));

            builder.Property(x => x.IsAvailable)
                .HasColumnName("available");

            builder.AddAggregateRootProperties<CatalogItem, CatalogItemId>();
        }
    }
}
