using System;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class Train : RollingStock
    {
        private Train() { }

        public Train(
            RollingStockId rollingStockId,
            Railway railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Couplers? couplers,
            string? typeName,
            string? livery,
            DccInterface dccInterface,
            Control control)
        {
            Id = rollingStockId;
            TypeName = typeName;
            Railway = railway;
            Category = category;
            Epoch = epoch;
            Length = length;
            MinRadius = minRadius;
            Couplers = couplers;
            Livery = livery;
            DccInterface = dccInterface;
            Control = control;
        }

        public string? TypeName { get; }
        public DccInterface DccInterface { get; }
        public Control Control { get; }

        public Train With(
            Railway? railway = null,
            Category? category = null,
            Epoch? epoch = null,
            LengthOverBuffer? length = null,
            MinRadius? minRadius = null,
            Couplers? couplers = null,
            string? typeName = null,
            string? livery = null,
            DccInterface? dccInterface = null,
            Control? control = null)
        {
            return new Train(
                Id,
                railway ?? Railway,
                category ?? Category,
                epoch ?? Epoch,
                length ?? Length,
                minRadius ?? MinRadius,
                couplers ?? Couplers,
                typeName ?? TypeName,
                livery ?? Livery,
                dccInterface ?? DccInterface,
                control ?? Control);
        }
    }
}
