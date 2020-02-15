using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

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
                .HasConversion<Guid>(
                    id => id.ToGuid(),
                    guid => new ScaleId(guid))
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(e => e.Slug)
                .HasConversion<string>(
                    slug => slug.ToString(),
                    str => Slug.Of(str))
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(10);

            builder.Property(e => e.Gauge)
                .HasConversion<decimal>(
                    g => g.ToDecimal(MeasureUnit.Millimeters),
                    g => Gauge.OfMillimiters(g))
                .IsRequired();

            builder.Property(e => e.Ratio)
                .HasConversion<float>(
                    r => r.ToFloat(),
                    r => Ratio.Of(r))
                .IsRequired();

            builder.Property(e => e.TrackGauge)
                .HasConversion<string>(
                    tg => tg.ToString(),
                    str => str.ToTrackGauge())
                .HasMaxLength(15);
        }
    }
}
