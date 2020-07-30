using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class FreightCar : RollingStock
    {
        private FreightCar()
        {
        }

        public FreightCar(
            RollingStockId rollingStockId,
            Railway railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Couplers? couplers,
            string? typeName,
            string? livery)
        {
            Id = rollingStockId;
            Railway = railway;
            Category = category;
            Epoch = epoch;
            Length = length;
            MinRadius = minRadius;
            Couplers = couplers;
            TypeName = typeName;
            Livery = livery;
        }

        public string? TypeName { get; }

        public FreightCar With(
            Railway? railway = null,
            Epoch? epoch = null,
            LengthOverBuffer? length = null,
            MinRadius? minRadius = null,
            Couplers? couplers = null,
            string? typeName = null,
            string? livery = null)
        {
            return new FreightCar(
                Id,
                railway ?? Railway,
                Category,
                epoch ?? Epoch,
                length ?? Length,
                minRadius ?? MinRadius,
                couplers ?? Couplers,
                typeName ?? TypeName,
                livery ?? Livery);
        }
    }
}
