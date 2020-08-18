namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class NewRollingStockInput
    {
        public static readonly RollingStockInput Empty = With();

        public static RollingStockInput With(
            string epoch = null, string category = null, string railway = null,
            string className = null, string roadNumber = null, string typeName = null, string series = null,
            string couplers = null,
            string livery = null, string depot = null,
            string passengerCarType = null, string serviceLevel = null,
            LengthOverBufferInput length = null,
            decimal? minRadius = null,
            string control = null, string dccInterface = null) =>
            new RollingStockInput(epoch, category, railway,
                className, roadNumber, typeName, series, couplers, livery, depot,
                passengerCarType, serviceLevel,
                length,
                minRadius,
                control, dccInterface);
    }
}
