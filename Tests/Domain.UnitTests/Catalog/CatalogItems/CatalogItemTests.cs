using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemTests
    {
        [Fact]
        public void CatalogItem_shouldHaveDefaultIdAndSlug()
        {
            var newItem = new CatalogItem(Brand(), new ItemNumber("123456"), Scale(), null, PowerMethod.DC, "", null, null);
            Assert.NotEqual(Guid.Empty, newItem.CatalogItemId.ToGuid());
            Assert.Equal(Slug.Of("acme-123456"), newItem.Slug);
        }

        private static IBrand Brand()
        {
            return new Brand(BrandId.NewId(), "ACME", Slug.Of("ACME"), null, null, null, BrandKind.Industrial);
        }

        private static IScale Scale()
        {
            return new Scale(ScaleId.NewId(), Slug.Of("H0"), "H0", Ratio.Of(87M), Gauge.OfMillimiters(16.5M), TrackGauge.Standard, null);
        }
    }
}