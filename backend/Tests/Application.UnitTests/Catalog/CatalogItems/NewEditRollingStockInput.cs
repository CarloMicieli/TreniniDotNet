using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class NewEditRollingStockInput
    {
        public static readonly EditRollingStockInput Empty = With();

        public static EditRollingStockInput With(
            Slug? slug = null, RollingStockId? rollingStockId = null,
            string epoch = null, string category = null, string railway = null,
            string className = null, string roadNumber = null, string typeName = null, string series = null,
            string couplers = null,
            string livery = null, string depot = null,
            string passengerCarType = null, string serviceLevel = null,
            LengthOverBufferInput length = null,
            decimal? minRadius = null,
            string control = null, string dccInterface = null) =>
            new EditRollingStockInput(
                slug ?? Slug.Empty,
                rollingStockId ?? RollingStockId.Empty,
                epoch, category, railway,
                className, roadNumber, typeName, passengerCarType, serviceLevel, series,
                couplers,
                livery,
                depot,
                length,
                minRadius,
                control, dccInterface);
    }
}
