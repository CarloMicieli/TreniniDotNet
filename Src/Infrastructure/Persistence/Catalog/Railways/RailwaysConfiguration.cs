using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasMaxLength(25);

            builder.Property(e => e.Status)
                .HasMaxLength(10);
        }
    }
}
