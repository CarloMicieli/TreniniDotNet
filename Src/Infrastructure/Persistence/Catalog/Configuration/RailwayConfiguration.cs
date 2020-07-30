#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Lengths;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public sealed class RailwayConfiguration : IEntityTypeConfiguration<Railway>
    {
        public void Configure(EntityTypeBuilder<Railway> builder)
        {
            builder.ToTable("railways");
            builder.HasKey(x => x.Id);

            builder.HasIndex(b => b.Slug).IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("railway_id")
                .HasConversion(
                    id => (Guid)id,
                    guid => new RailwayId(guid));

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

            builder.Property(x => x.CompanyName)
                .HasColumnName("company_name")
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(x => x.Country)
                .HasColumnName("country")
                .HasMaxLength(2)
                .IsRequired()
                .HasConversion(
                    country => country.Code,
                    s => Country.Of(s));

            builder.OwnsOne(x => x.PeriodOfActivity, poa =>
            {
                poa.Property(x => x.OperatingSince)
                    .HasColumnName("operating_since");

                poa.Property(x => x.OperatingUntil)
                    .HasColumnName("operating_until");

                poa.Property(x => x.RailwayStatus)
                    .HasColumnName("active")
                    .HasConversion(
                        status => status == RailwayStatus.Active,
                        active => active ? RailwayStatus.Active : RailwayStatus.Inactive);
            });

            builder.OwnsOne(x => x.TrackGauge, gauge =>
            {
                gauge.Property(x => x.TrackGauge)
                    .HasColumnName("track_gauge")
                    .HasMaxLength(10)
                    .HasConversion(
                        gauge => gauge.ToString(),
                        s => EnumHelpers.RequiredValueFor<TrackGauge>(s));

                gauge.Property(x => x.Inches)
                    .HasColumnName("gauge_in")
                    .HasConversion(
                        len => len.Value,
                        d => Length.OfInches(d));

                gauge.Property(x => x.Millimeters)
                    .HasColumnName("gauge_mm")
                    .HasConversion(
                        len => len.Value,
                        d => Length.OfMillimeters(d));
            });

            builder.OwnsOne(x => x.TotalLength, tl =>
            {
                tl.Property(x => x.Kilometers)
                    .HasColumnName("total_length_km")
                    .HasConversion(
                        km => km.Value,
                        d => Length.OfKilometers(d));

                tl.Property(x => x.Miles)
                    .HasColumnName("total_length_mi")
                    .HasConversion(
                        mi => mi.Value,
                        d => Length.OfMiles(d));
            });

            builder.Property(x => x.WebsiteUrl)
                .HasColumnName("website_url")
                .IsUnicode()
                .HasMaxLength(255)
                .HasConversion(
                    uri => uri.ToString(),
                    s => new Uri(s));

            builder.Property(x => x.Headquarters)
                .HasColumnName("headquarters")
                .IsUnicode()
                .HasMaxLength(100);

            builder.AddAggregateRootProperties<Railway, RailwayId>();
        }
    }
}
