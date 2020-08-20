#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration
{
    public class PassengerCarConfiguration : IEntityTypeConfiguration<PassengerCar>
    {
        public void Configure(EntityTypeBuilder<PassengerCar> builder)
        {
            builder.Property(x => x.TypeName)
                .HasColumnName("type_name")
                .HasMaxLength(15)
                .IsUnicode();

            builder.Property(x => x.PassengerCarType)
                .HasColumnName("passenger_car_type")
                .HasMaxLength(25)
                .HasConversion(
                    carType => carType.ToString(),
                    s => EnumHelpers.RequiredValueFor<PassengerCarType>(s));

            builder.Property(x => x.ServiceLevel)
                .HasColumnName("service_level")
                .HasMaxLength(10)
                .HasConversion(
                    level => level.ToString(),
                    s => ServiceLevel.Parse(s));
        }
    }
}
