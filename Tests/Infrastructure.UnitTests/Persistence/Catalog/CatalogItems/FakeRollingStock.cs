using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class FakeRollingStock : IRollingStock
    {
        private RollingStockId _id;

        public FakeRollingStock(Guid id)
        {
            _id = new RollingStockId(id);
        }

        public RollingStockId RollingStockId => _id;

        public IRailwayInfo Railway => new FakeRailwayInfo();

        public Category Category => Category.ElectricLocomotive;

        public Epoch Epoch => Epoch.IV;

        public string ClassName => "class name";

        public string RoadNumber => "road num";

        public string TypeName => "type name";

        public PassengerCarType? PassengerCarType => null;

        public ServiceLevel ServiceLevel => null;

        public DccInterface DccInterface => DccInterface.Nem651;

        public Control Control => Control.DccReady;

        public LengthOverBuffer Length => LengthOverBuffer.Create(null, 210M);
    }
}
