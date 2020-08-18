using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class TrainsBuilder : RollingStocksBuilder<TrainsBuilder, Train>
    {
        private string _typeName;
        private DccInterface _dccInterface;
        private Control _control;

        protected override TrainsBuilder Instance => this;

        public TrainsBuilder TypeName(string typeName)
        {
            _typeName = typeName;
            return this;
        }

        public TrainsBuilder DccInterface(DccInterface dccInterface)
        {
            _dccInterface = dccInterface;
            return this;
        }

        public TrainsBuilder Control(Control control)
        {
            _control = control;
            return this;
        }

        public override Train Build()
        {
            return new Train(
                _id,
                _railway,
                _category,
                _epoch,
                _lengthOverBuffer,
                _minRadius,
                _couplers,
                _typeName,
                _livery,
                _dccInterface,
                _control);
        }
    }
}
