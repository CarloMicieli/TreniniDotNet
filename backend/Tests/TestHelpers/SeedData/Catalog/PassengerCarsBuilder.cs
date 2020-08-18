using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class PassengerCarsBuilder : RollingStocksBuilder<PassengerCarsBuilder, PassengerCar>
    {
        private string _typeName;
        private PassengerCarType? _passengerCarType;
        private ServiceLevel _serviceLevel;

        protected override PassengerCarsBuilder Instance => this;

        public PassengerCarsBuilder ServiceLevel(ServiceLevel serviceLevel)
        {
            _serviceLevel = serviceLevel;
            return this;
        }

        public PassengerCarsBuilder TypeName(string typeName)
        {
            _typeName = typeName;
            return this;
        }

        public PassengerCarsBuilder PassengerCarType(PassengerCarType passengerCarType)
        {
            _passengerCarType = passengerCarType;
            return this;
        }

        public override PassengerCar Build()
        {
            return new PassengerCar(
                _id,
                _railway,
                _category,
                _epoch,
                _lengthOverBuffer,
                _minRadius,
                _couplers,
                _typeName,
                _livery,
                _passengerCarType,
                _serviceLevel);
        }
    }
}