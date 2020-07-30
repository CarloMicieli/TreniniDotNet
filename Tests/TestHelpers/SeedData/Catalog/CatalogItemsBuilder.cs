using System;
using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class CatalogItemsBuilder
    {
        private CatalogItemId _id;
        private Brand _brand;
        private ItemNumber _itemNumber;
        private Scale _scale;
        private PowerMethod _powerMethod;
        private string _description;
        private string _prototypeDescription;
        private string _modelDescription;
        private DeliveryDate? _deliveryDate;
        private bool _available;
        private List<RollingStock> _rollingStocks;
        private readonly Instant _created;
        private readonly Instant? _modified;
        private readonly int _version;

        public CatalogItemsBuilder()
        {
            _id = CatalogItemId.NewId();
            _rollingStocks = new List<RollingStock>();
            _created = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _modified = null;
            _version = 1;
        }

        public CatalogItemsBuilder Id(Guid id)
        {
            _id = new CatalogItemId(id);
            return this;
        }

        public CatalogItemsBuilder ItemNumber(ItemNumber itemNumber)
        {
            _itemNumber = itemNumber;
            return this;
        }

        public CatalogItemsBuilder PowerMethod(PowerMethod powerMethod)
        {
            _powerMethod = powerMethod;
            return this;
        }

        public CatalogItemsBuilder DeliveryDate(DeliveryDate deliveryDate)
        {
            _deliveryDate = deliveryDate;
            return this;
        }

        public CatalogItemsBuilder Description(string description)
        {
            _description = description;
            return this;
        }

        public CatalogItemsBuilder PrototypeDescription(string prototypeDescription)
        {
            _prototypeDescription = prototypeDescription;
            return this;
        }

        public CatalogItemsBuilder ModelDescription(string modelDescription)
        {
            _modelDescription = modelDescription;
            return this;
        }

        public CatalogItemsBuilder Brand(Brand brand)
        {
            _brand = brand;
            return this;
        }

        public CatalogItemsBuilder Scale(Scale scale)
        {
            _scale = scale;
            return this;
        }

        public CatalogItemsBuilder Available()
        {
            _available = true;
            return this;
        }

        public CatalogItemsBuilder Unavailable()
        {
            _available = false;
            return this;
        }


        public CatalogItemsBuilder RollingStock(RollingStock rs)
        {
            _rollingStocks.Add(rs);
            return this;
        }

        public CatalogItemsBuilder Locomotive(Action<LocomotivesBuilder> buildAction)
        {
            var builder = new LocomotivesBuilder();
            buildAction(builder);
            return RollingStock(builder.Build());
        }

        public CatalogItemsBuilder PassengerCar(Action<PassengerCarsBuilder> buildAction)
        {
            var builder = new PassengerCarsBuilder();
            buildAction(builder);
            return RollingStock(builder.Build());
        }

        public CatalogItemsBuilder FreightCar(Action<FreightCarsBuilder> buildAction)
        {
            var builder = new FreightCarsBuilder();
            buildAction(builder);
            return RollingStock(builder.Build());
        }

        public CatalogItemsBuilder Train(Action<TrainsBuilder> buildAction)
        {
            var builder = new TrainsBuilder();
            buildAction(builder);
            return RollingStock(builder.Build());
        }

        public CatalogItem Build()
        {
            return new CatalogItem(
                _id,
                _brand,
                _itemNumber,
                _scale,
                _powerMethod,
                _description,
                _prototypeDescription,
                _modelDescription,
                _deliveryDate,
                _available,
                _rollingStocks,
                _created,
                _modified,
                _version);
        }
    }
}
