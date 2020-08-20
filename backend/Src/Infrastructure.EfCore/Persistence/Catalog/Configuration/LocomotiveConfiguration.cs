#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public class LocomotiveConfiguration : IEntityTypeConfiguration<Locomotive>
    {
        public void Configure(EntityTypeBuilder<Locomotive> builder)
        {
            builder.Property(x => x.DccInterface)
                .HasColumnName("dcc_interface")
                .HasMaxLength(10)
                .HasConversion(
                    dcc => dcc.ToString(),
                    s => EnumHelpers.RequiredValueFor<DccInterface>(s));

            builder.Property(x => x.Control)
                .HasColumnName("control")
                .HasMaxLength(10)
                .HasConversion(
                    c => c.ToString(),
                    s => EnumHelpers.RequiredValueFor<Control>(s));

            builder.Property(x => x.Depot)
                .HasColumnName("depot")
                .HasMaxLength(100)
                .IsUnicode();

            builder.OwnsOne(x => x.Prototype, p =>
            {
                p.Property(x => x.ClassName)
                    .HasColumnName("class_name")
                    .HasMaxLength(15)
                    .IsUnicode();

                p.Property(x => x.RoadNumber)
                    .HasColumnName("road_number")
                    .HasMaxLength(15)
                    .IsUnicode();

                p.Property(x => x.Series)
                    .HasColumnName("series")
                    .HasMaxLength(25);
            });
        }
    }
}
