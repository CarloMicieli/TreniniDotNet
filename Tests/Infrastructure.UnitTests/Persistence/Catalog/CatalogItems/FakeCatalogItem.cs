using NodaTime;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class FakeCatalogItem : ICatalogItem
    {
        public FakeCatalogItem(IBrandInfo brand)
        {
            CatalogItemId = new CatalogItemId(Guid.NewGuid());

            RollingStocks = ImmutableList.Create<IRollingStock>(
                new FakeRollingStock(Guid.NewGuid()));

            Brand = brand;
        }

        public CatalogItemId CatalogItemId { get; }

        public IBrandInfo Brand { get; }

        public Slug Slug => Slug.Of("acme", "123456");

        public ItemNumber ItemNumber => new ItemNumber("123456");

        public IReadOnlyList<IRollingStock> RollingStocks { get; }

        public string Description => "Description";

        public string PrototypeDescription => null;

        public string ModelDescription => null;

        public IScaleInfo Scale => new FakeScaleInfo();

        public PowerMethod PowerMethod => PowerMethod.DC;

        public DeliveryDate? DeliveryDate => null;

        public bool IsAvailable => false;

        public Instant CreatedDate => Instant.FromUtc(1988, 11, 25, 9, 0);

        public Instant? ModifiedDate => null;

        public int Version => 1;

        public ICatalogItemInfo ToCatalogItemInfo() => this;
    }
}
