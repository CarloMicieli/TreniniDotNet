using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class Locomotive : RollingStock
    {
        private Locomotive() { }

        public Locomotive(
            RollingStockId rollingStockId,
            Railway railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Prototype? prototype,
            Couplers? couplers,
            string? livery,
            string? depot,
            DccInterface dccInterface,
            Control control)
        {
            Id = rollingStockId;
            Railway = railway;
            Category = category;
            Epoch = epoch;
            Length = length;
            MinRadius = minRadius;
            Prototype = prototype;
            Couplers = couplers;
            Livery = livery;
            Depot = depot;
            DccInterface = dccInterface;
            Control = control;
        }

        public string? Depot { get; }
        public DccInterface DccInterface { get; }
        public Control Control { get; }
        public Prototype? Prototype { get; }

        public Locomotive With(
            Railway? railway = null,
            Category? category = null,
            Epoch? epoch = null,
            LengthOverBuffer? length = null,
            MinRadius? minRadius = null,
            Prototype? prototype = null,
            Couplers? couplers = null,
            string? livery = null,
            string? depot = null,
            DccInterface? dccInterface = null,
            Control? control = null)
        {
            return new Locomotive(
                Id,
                railway ?? Railway,
                category ?? Category,
                epoch ?? Epoch,
                length ?? Length,
                minRadius ?? MinRadius,
                prototype ?? Prototype,
                couplers ?? Couplers,
                livery ?? Livery,
                depot ?? Depot,
                dccInterface ?? DccInterface,
                control ?? Control);
        }
    }
}
