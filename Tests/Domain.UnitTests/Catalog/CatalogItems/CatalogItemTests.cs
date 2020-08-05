using System;
using System.Collections.Generic;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemTests
    {
        private CatalogItemId ExpectedItemId { get; }

        public CatalogItemTests()
        {
            ExpectedItemId = new CatalogItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"));
        }

        [Fact]
        public void CatalogItem_ShouldCreateNewValues()
        {
            var catalogItem = new CatalogItem(
                ExpectedItemId,
                new BrandRef(CatalogSeedData.Brands.NewAcme()),
                new ItemNumber("60392"),
                new ScaleRef(CatalogSeedData.Scales.ScaleH0()),
                PowerMethod.DC,
                "FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.",
                "Prototype desc goes here",
                "Model desc goes here",
                DeliveryDate.FirstQuarterOf(2019),
                true,
                new List<RollingStock>(),
                Instant.FromUtc(2020, 11, 25, 10, 0),
                null,
                1);

            catalogItem.Should().NotBeNull();
            catalogItem.Id.Should().Be(ExpectedItemId);
            catalogItem.Slug.Should().Be(Slug.Of("acme-60392"));
            catalogItem.ItemNumber.Should().Be(new ItemNumber("60392"));
            catalogItem.Scale.Should().Be(CatalogSeedData.Scales.ScaleH0());
            catalogItem.Brand.Should().Be(CatalogSeedData.Brands.NewAcme());
            catalogItem.Description.Should().Be("FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.");
            catalogItem.ModelDescription.Should().Be("Model desc goes here");
            catalogItem.PrototypeDescription.Should().Be("Prototype desc goes here");
            catalogItem.DeliveryDate.Should().Be(DeliveryDate.FirstQuarterOf(2019));
            catalogItem.PowerMethod.Should().Be(PowerMethod.DC);
            catalogItem.IsAvailable.Should().BeTrue();
            catalogItem.RollingStocks.Should().HaveCount(0);
            catalogItem.CreatedDate.Should().Be(Instant.FromUtc(2020, 11, 25, 10, 0));
            catalogItem.Version.Should().Be(1);
        }

        [Fact]
        public void CatalogItem_ShouldCheckForEquality()
        {
            var item1 = CatalogSeedData.CatalogItems.NewAcme60392();
            var item2 = CatalogSeedData.CatalogItems.NewRivarossiHR4298();

            (item1 == item2).Should().BeFalse();
            (item1 != item2).Should().BeTrue();
            (item1.Equals(item2)).Should().BeFalse();
        }

        [Fact]
        public void CatalogItem_With_ShouldProduceNewModifiedValues()
        {
            var modified = CatalogSeedData.CatalogItems.NewAcme60458()
                .With(prototypeDescription: "modified prototype desc");

            modified.Should().NotBeNull();
            modified.Should().NotBeSameAs(CatalogSeedData.CatalogItems.NewAcme60458());
            modified.PrototypeDescription.Should().Be("modified prototype desc");
        }

        [Fact]
        public void CatalogItem_With_ShouldChangeTheCatalogItemSlug()
        {
            var modified = CatalogSeedData.CatalogItems.NewAcme60458()
                .With(itemNumber: new ItemNumber("123456"));

            modified.Should().NotBeNull();
            modified.Slug.Should().Be(Slug.Of("acme-123456"));
        }

        [Fact]
        public void CatalogItem_ToString_ShouldProduceStringRepresentations()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60458();
            catalogItem.ToString().Should().Be("CatalogItem(ACME 60458)");
        }

        [Fact]
        public void CatalogItem_Count_ShouldCountTheRollingStocks()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60458();
            catalogItem.Count.Should().Be(1);
        }

        [Fact]
        public void CatalogItem_Category_ShouldCalculateCategoryFromRollingStocks()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60458();

            catalogItem.Category.Should().Be(CatalogItemCategory.Locomotives);
        }
    }
}
