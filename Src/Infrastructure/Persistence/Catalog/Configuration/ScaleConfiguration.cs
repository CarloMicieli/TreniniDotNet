using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public sealed class ScaleConfiguration : IEntityTypeConfiguration<Scale>
    {
        public void Configure(EntityTypeBuilder<Scale> builder)
        {
            builder.ToTable("scales");
            builder.HasKey(x => x.Id);

            builder.HasIndex(b => b.Slug).IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("scale_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new ScaleId(guid));

            builder.Property(x => x.Slug)
                .HasColumnName("slug")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50)
                .HasConversion(
                    slug => slug.Value,
                    s => Slug.Of(s));

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.Ratio)
                .HasColumnName("ratio")
                .IsRequired()
                .HasConversion(
                    ratio => ratio.ToDecimal(),
                    d => Ratio.Of(d));

            builder.OwnsOne(x => x.Gauge, g =>
            {
                g.Property(x => x.InMillimeters)
                    .HasColumnName("gauge_mm")
                    .HasConversion(
                        gauge => gauge.Value,
                        d => Gauge.OfMillimeters(d));

                g.Property(x => x.InInches)
                    .HasColumnName("gauge_in")
                    .HasConversion(
                        gauge => gauge.Value,
                        d => Gauge.OfInches(d));

                g.Property(x => x.TrackGauge)
                    .HasColumnName("track_type")
                    .HasConversion(
                        tg => tg.ToString(),
                        s => EnumHelpers.RequiredValueFor<TrackGauge>(s));
            });

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(4000);

            builder.Property(x => x.Weight)
                .HasColumnName("weight");

            // builder.Property(x => x.Standards)
            //     .HasColumnName("standards")
            //     .HasMaxLength(100)
            //     .HasConversion(
            //         set => string.Join(',', set),
            //         s => ImmutableHashSet<ScaleStandard>.Empty);

            builder.AddAggregateRootProperties<Scale, ScaleId>();
        }
    }
}
