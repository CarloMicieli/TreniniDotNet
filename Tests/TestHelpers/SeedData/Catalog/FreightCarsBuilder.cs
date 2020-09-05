using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class FreightCarsBuilder : RollingStocksBuilder<FreightCarsBuilder, FreightCar>
    {
        private string _typeName;

        protected override FreightCarsBuilder Instance => this;

        public FreightCarsBuilder TypeName(string typeName)
        {
            _typeName = typeName;
            return this;
        }

        public override FreightCar Build()
        {
            return new FreightCar(
                _id,
                _railway,
                _category,
                _epoch,
                _lengthOverBuffer,
                _minRadius,
                _couplers,
                _typeName,
                _livery);
        }
    }
}