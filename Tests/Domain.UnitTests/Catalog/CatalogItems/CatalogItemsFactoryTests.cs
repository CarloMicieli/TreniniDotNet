using System;
using FluentAssertions;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemsFactoryTests
    {
        private CatalogItemsFactory Factory { get; }
        private CatalogItemId ExpectedId { get; }

        public CatalogItemsFactoryTests()
        {
            var id = Guid.NewGuid();

            ExpectedId = new CatalogItemId(id);

            Factory = Factories<CatalogItemsFactory>
                .New((clock, guidSource) => new CatalogItemsFactory(clock, guidSource))
                .Id(id)
                .Build();
        }

        [Fact]
        public void CatalogItemsFactory_ShouldCreateNewCatalogItems()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            var newItem = Factory.CreateCatalogItem(
                item.Brand,
                item.ItemNumber,
                item.Scale,
                item.PowerMethod,
                item.Description,
                item.PrototypeDescription,
                item.ModelDescription,
                item.DeliveryDate,
                item.IsAvailable,
                item.RollingStocks);

            newItem.Should().NotBeNull();
            newItem.Id.Should().Be(ExpectedId);
        }
    }
}
