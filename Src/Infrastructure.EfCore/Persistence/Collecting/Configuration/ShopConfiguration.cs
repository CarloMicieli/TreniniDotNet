#nullable disable
using System;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("shops");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("shop_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new ShopId(guid));

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasColumnName("slug")
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired()
                .HasConversion(
                    slug => slug.Value,
                    s => Slug.Of(s));

            builder.Property(x => x.WebsiteUrl)
                .HasColumnName("website_url")
                .HasMaxLength(100)
                .HasConversion(
                    url => url.ToString(),
                    s => new Uri(s));

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(50)
                .HasConversion(
                    phone => phone.Value.ToString(),
                    s => PhoneNumber.Of(s));

            builder.Property(x => x.EmailAddress)
                .HasColumnName("mail_address")
                .HasMaxLength(100)
                .HasConversion(
                    mail => mail.ToString(),
                    s => new MailAddress(s));

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

            builder.AddAggregateRootProperties<Shop, ShopId>();

        }
    }
}
