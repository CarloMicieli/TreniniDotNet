using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public class TrainConfiguration : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.Property(x => x.DccInterface)
                .HasColumnName("dcc_interface")
                .HasMaxLength(10)
                .HasConversion(
                    dcc => dcc.ToString(),
                    s => EnumHelpers.RequiredValueFor<DccInterface>(s));

            builder.Property(x => x.Control)
                .HasColumnName("control")
                .HasMaxLength(10)
                .HasConversion(
                    c => c.ToString(),
                    s => EnumHelpers.RequiredValueFor<Control>(s));
        }
    }
}
