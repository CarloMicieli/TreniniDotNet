using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;

namespace TreniniDotNet.Infrastracture.Persistence.Catalog.CatalogItems
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasIndex(e => e.Slug)
                .IsUnique()
                .HasName("Idx_CatalogItems_Slug");

            builder.HasIndex(e => e.ItemNumber)
                .IsUnique(false)
                .HasName("Idx_CatalogItems_ItemNumber_BrandName");

            builder.Property(e => e.ItemNumber)
                .IsUnicode(false)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Slug)
                .IsUnicode(false)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(e => e.Category)
                .IsUnicode(false)
                .IsRequired(true)
                .HasMaxLength(25);

            builder.Property(e => e.Description)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(250);

            builder.Property(e => e.PrototypeDescription)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(2500);

            builder.Property(e => e.ModelDescription)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(2500);

            builder.Property(e => e.DeliveryDate)
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(10);
        }
    }
}
