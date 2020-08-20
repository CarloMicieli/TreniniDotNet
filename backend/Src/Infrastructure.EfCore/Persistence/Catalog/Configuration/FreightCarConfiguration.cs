#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public class FreightCarConfiguration : IEntityTypeConfiguration<FreightCar>
    {
        public void Configure(EntityTypeBuilder<FreightCar> builder)
        {
            builder.Property(x => x.TypeName)
                .HasColumnName("type_name")
                .HasMaxLength(15)
                .IsUnicode();
        }
    }
}
