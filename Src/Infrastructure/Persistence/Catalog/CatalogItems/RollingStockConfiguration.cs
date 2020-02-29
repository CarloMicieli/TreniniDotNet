using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TreniniDotNet.Infrastracture.Persistence.Catalog.CatalogItems
{
    public class RollingStockConfiguration : IEntityTypeConfiguration<RollingStock>
    {
        public void Configure(EntityTypeBuilder<RollingStock> builder)
        {
            builder.Property(e => e.Era)
                .IsUnicode(false)
                .HasMaxLength(5);

            builder.Property(e => e.Length)
                .IsRequired(false);

            builder.Property(e => e.ClassName)
                .IsUnicode(false)
                .HasMaxLength(25);

            builder.Property(e => e.RoadNumber)
                .IsUnicode(false)
                .HasMaxLength(25);

            builder.Property(e => e.Category)
                .IsUnicode(false)
                .IsRequired(true)
                .HasMaxLength(25);                

            builder.Property(e => e.Livery)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(e => e.DepotName)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(e => e.Series)
                .IsUnicode(false)
                .HasMaxLength(25);
        }
    }
}
