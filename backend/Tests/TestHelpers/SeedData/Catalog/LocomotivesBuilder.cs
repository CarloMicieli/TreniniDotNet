using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class LocomotivesBuilder : RollingStocksBuilder<LocomotivesBuilder, Locomotive>
    {
        private string _depot;
        private Prototype _prototype;
        private DccInterface _dccInterface;
        private Control _control;

        protected override LocomotivesBuilder Instance => this;

        public LocomotivesBuilder Prototype(Prototype prototype)
        {
            _prototype = prototype;
            return this;
        }

        public LocomotivesBuilder Depot(string depot)
        {
            _depot = depot;
            return this;
        }

        public LocomotivesBuilder DccInterface(DccInterface dccInterface)
        {
            _dccInterface = dccInterface;
            return this;
        }

        public LocomotivesBuilder Control(Control control)
        {
            _control = control;
            return this;
        }

        public override Locomotive Build()
        {
            return new Locomotive(
                _id,
                _railway,
                _category,
                _epoch,
                _lengthOverBuffer,
                _minRadius,
                _prototype,
                _couplers,
                _livery,
                _depot,
                _dccInterface,
                _control);
        }
    }
}