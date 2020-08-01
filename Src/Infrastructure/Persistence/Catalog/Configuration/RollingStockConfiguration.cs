#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Lengths;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public sealed class RollingStockConfiguration : IEntityTypeConfiguration<RollingStock>
    {
        public void Configure(EntityTypeBuilder<RollingStock> builder)
        {
            builder.ToTable("rolling_stocks");
            builder.HasKey(x => x.Id);

            builder.HasDiscriminator<string>("rolling_stock_type");
            
            builder.Property(x => x.Id)
                .HasColumnName("rolling_stock_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new RollingStockId(guid));

            builder.Property("CatalogItemId")
                .HasColumnName("catalog_item_id")
                .IsRequired();

            builder.HasOne(x => x.Railway);
            builder.Property<RailwayId>("RailwayId")
                .HasColumnName("railway_id")
                .IsRequired()
                .HasConversion(
                    id => (Guid)id,
                    guid => new RailwayId(guid));
 
            builder.OwnsOne(x => x.Length, length =>
            {
                length.Property(x => x.Inches)
                    .HasColumnName("length_in")
                    .HasConversion(
                        len => len.Value,
                        d => Length.OfInches(d));
            
                length.Property(x => x.Millimeters)
                    .HasColumnName("length_mm")
                    .HasConversion(
                        len => len.Value,
                        d => Length.OfMillimeters(d));
            });
            
            builder.Property(x => x.Category)
                .HasColumnName("category")
                .HasMaxLength(25)
                .IsRequired()
                .HasConversion(
                    cat => cat.ToString(),
                    s => EnumHelpers.RequiredValueFor<Category>(s));

            builder.Property(x => x.Epoch)
                .HasColumnName("epoch")
                .HasMaxLength(10)
                .IsRequired()
                .HasConversion(
                    ep => ep.ToString(),
                    s => Epoch.Parse(s));

            builder.Property(x => x.MinRadius)
                .HasColumnName("min_radius")
                .HasConversion(
                    rad => rad.Millimeters,
                    d => MinRadius.OfMillimeters(d));

            builder.Property(x => x.Couplers)
                .HasColumnName("couplers")
                .HasMaxLength(25)
                .HasConversion(
                    couplers => couplers.ToString(),
                    s => EnumHelpers.RequiredValueFor<Couplers>(s));

            builder.Property(x => x.Livery)
                .HasColumnName("livery")
                .HasMaxLength(50)
                .IsUnicode();
        }
    }
}
