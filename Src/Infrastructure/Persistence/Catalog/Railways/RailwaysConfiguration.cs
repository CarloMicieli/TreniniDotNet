using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    internal class RailwaysConfiguration : IEntityTypeConfiguration<Railway>
    {
        public void Configure(EntityTypeBuilder<Railway> builder)
        {
            builder.HasIndex(e => e.Name)
                .IsUnique()
                .HasName("Idx_Railways_Name");

            builder.HasIndex(e => e.Slug)
                .IsUnique()
                .HasName("Idx_Railways_Slug");

            builder.Property(e => e.RailwayId)
                .HasConversion<Guid>(
                    rid => rid.ToGuid(),
                    guid => new RailwayId(guid))
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.Property(e => e.CompanyName)
                .IsUnicode(true)
                .HasMaxLength(100);

            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(25)
                .HasConversion<string>(
                    slug => slug.ToString(),
                    str => Slug.Of(str));

            builder.Property(e => e.Status)
                .HasConversion<string>(
                    status => status.ToString(),
                    str => str.ToRailwayStatus())
                .HasMaxLength(10);
        }
    }
}
