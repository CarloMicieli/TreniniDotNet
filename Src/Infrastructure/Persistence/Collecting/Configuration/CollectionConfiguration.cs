#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("collections");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("collection_id")
                .HasConversion<Guid>(
                    id => id,
                    guid => new CollectionId(guid));

            builder.HasIndex(b => b.Owner).IsUnique(false);

            builder.Property(x => x.Owner)
                .HasColumnName("owner")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode()
                .HasConversion(
                    o => o.ToString(),
                    s => new Owner(s));

            builder.HasMany(x => x.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);


            builder.AddAggregateRootProperties<Collection, CollectionId>();
        }
    }
}
