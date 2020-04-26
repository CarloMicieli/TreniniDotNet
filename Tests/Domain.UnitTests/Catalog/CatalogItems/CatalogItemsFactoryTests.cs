using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemsFactoryTests
    {
        private ICatalogItemsFactory Factory { get; }
        private Instant ExpectedDateTime = Instant.FromUtc(1988, 11, 25, 0, 0);
        private CatalogItemId ExpectedItemId = new CatalogItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"));

        public CatalogItemsFactoryTests()
        {
            Factory = new CatalogItemsFactory(
                new FakeClock(ExpectedDateTime),
                FakeGuidSource.NewSource(ExpectedItemId.ToGuid()));
        }

        [Fact]
        public void CatalogItemsFactory_CreateNewCatalogItem_ShouldCreateNewCatalogItems()
        {
            var catalogItem = Factory.CreateNewCatalogItem(
                Acme(),
                new ItemNumber("60392"),
                H0(),
                PowerMethod.DC,
                RollingStocks().ToImmutableList(),
                "FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.",
                "Prototype desc goes here",
                "Model desc goes here",
                DeliveryDate.FirstQuarterOf(2019),
                true);

            catalogItem.Should().NotBeNull();
            catalogItem.CatalogItemId.Should().Be(ExpectedItemId);
            catalogItem.Slug.Should().Be(Slug.Of("acme-60392"));
            catalogItem.ItemNumber.Should().Be(new ItemNumber("60392"));
            catalogItem.Scale.Slug.Should().Be(H0().Slug);
            catalogItem.Brand.Slug.Should().Be(Acme().Slug);
            catalogItem.Description.Should().Be("FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.");
            catalogItem.ModelDescription.Should().Be("Model desc goes here");
            catalogItem.PrototypeDescription.Should().Be("Prototype desc goes here");
            catalogItem.DeliveryDate.Should().Be(DeliveryDate.FirstQuarterOf(2019));
            catalogItem.PowerMethod.Should().Be(PowerMethod.DC);
            catalogItem.RollingStocks.Should().HaveCount(RollingStocks().Count());
            catalogItem.CreatedDate.Should().Be(ExpectedDateTime);
            catalogItem.Version.Should().Be(1);
        }

        private static IScale H0() => CatalogSeedData.Scales.ScaleH0();

        private static IBrand Acme() => CatalogSeedData.Brands.Acme();

        private static IEnumerable<IRollingStock> RollingStocks() => CatalogSeedData.CatalogItems.Acme_60392().RollingStocks;
    }
}