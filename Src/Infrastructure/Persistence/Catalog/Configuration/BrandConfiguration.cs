#nullable disable
using System;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("brands");
            builder.HasKey(x => x.Id);

            builder.HasIndex(b => b.Slug).IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("brand_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new BrandId(guid));

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.Slug)
                .HasColumnName("slug")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50)
                .HasConversion(
                    slug => slug.Value,
                    s => Slug.Of(s));

            builder.Property(x => x.WebsiteUrl)
                .HasColumnName("website_url")
                .IsUnicode()
                .HasMaxLength(255)
                .HasConversion(
                    uri => uri.ToString(),
                    s => new Uri(s));

            builder.Property(x => x.EmailAddress)
                .HasColumnName("mail_address")
                .IsUnicode()
                .HasMaxLength(255)
                .HasConversion(
                    mail => mail.ToString(),
                    s => new MailAddress(s));

            builder.Property(x => x.CompanyName)
                .HasColumnName("company_name")
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(x => x.GroupName)
                .HasColumnName("group_name")
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsUnicode()
                .HasMaxLength(4000);

            builder.Property(x => x.Kind)
                .HasColumnName("kind")
                .HasMaxLength(10)
                .HasConversion(
                    kind => kind.ToString(),
                    s => EnumHelpers.RequiredValueFor<BrandKind>(s));

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(x => x.Line1)
                    .HasColumnName("address_line1")
                    .HasMaxLength(255)
                    .IsUnicode();

                a.Property(x => x.Line2)
                    .HasColumnName("address_line2")
                    .HasMaxLength(255)
                    .IsUnicode();

                a.Property(x => x.City)
                    .HasColumnName("address_city")
                    .HasMaxLength(50)
                    .IsUnicode();

                a.Property(x => x.Region)
                    .HasColumnName("address_region")
                    .HasMaxLength(50)
                    .IsUnicode();

                a.Property(x => x.PostalCode)
                    .HasColumnName("address_postal_code")
                    .HasMaxLength(10);

                a.Property(x => x.Country)
                    .HasColumnName("address_country")
                    .HasMaxLength(2);
            });

            builder.AddAggregateRootProperties<Brand, BrandId>();
        }
    }
}
