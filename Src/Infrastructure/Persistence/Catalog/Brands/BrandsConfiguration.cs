using System;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    internal class BrandsConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasIndex(e => e.Name)
                .IsUnique()
                .HasName("Idx_Brands_Name");

            builder.HasIndex(e => e.Slug)
                .IsUnique()
                .HasName("Idx_Brands_Slug");

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.Property(e => e.Slug)
                .HasConversion(
                    value => value.ToString(),
                    value => Slug.Of(value))
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(e => e.BrandId)
                .HasConversion<Guid>(
                    value => value.ToGuid(),
                    value => new BrandId(value))
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(e => e.EmailAddress)
                .HasConversion(
                    value => value!.ToString(),
                    value => new MailAddress(value))
                .HasMaxLength(250);

            builder.Property(e => e.WebsiteUrl)
                .HasConversion(
                    value => value!.ToString(),
                    value => new Uri(value))
                .HasMaxLength(250);

            builder.Property(e => e.Kind)
                .HasConversion<string>(
                    kind => kind.ToString(),
                    str => str.ToBrandKind())
                .HasMaxLength(25);
        }
    }
}
