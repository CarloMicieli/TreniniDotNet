using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    internal class ScalesConfiguration : IEntityTypeConfiguration<Scale>
    {
        public void Configure(EntityTypeBuilder<Scale> builder)
        {
            builder.HasIndex(e => e.Name)
                .IsUnique()
                .HasName("Idx_Scales_Name");

            builder.HasIndex(e => e.Slug)
                .IsUnique()
                .HasName("Idx_Scales_Slug");

            builder.Property(e => e.ScaleId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(e => e.Slug)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(10);

            builder.Property(e => e.Gauge)
                .IsRequired();

            builder.Property(e => e.Ratio)
                .IsRequired();

            builder.Property(e => e.TrackGauge)
                .HasMaxLength(15);
        }
    }
}
