using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using System.Linq;
using TreniniDotNet.Common.Uuid;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemsFactoryTests
    {
        private readonly ICatalogItemsFactory itemsFactory;

        public CatalogItemsFactoryTests()
        {
            itemsFactory = new CatalogItemsFactory(
                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)),
                FakeGuidSource.NewSource(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
        }

        [Fact]
        public void CatalogItemsFactory_ShouldCreateCatalogItems_WithASingleRollingStocks()
        {
            var success = itemsFactory.NewCatalogItem(
                Acme(),
                "60392",
                H0(),
                "dc",
                null,
                false,
                "FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.",
                null, null,
                RollingStock()
            );

            success.Match(
                Succ: succ =>
                {
                    succ.CatalogItemId.Should().Be(new CatalogItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
                    succ.Slug.Should().Be(Slug.Of("acme-60392"));
                },
                Fail: errors => Assert.True(false, "should never get here")
            );
        }

        private static IScale H0() => CatalogSeedData.Scales.ScaleH0();

        private static IBrand Acme() => CatalogSeedData.Brands.Acme();

        private static IRollingStock RollingStock() => CatalogSeedData.CatalogItems.Acme_60392().RollingStocks.First();

        private static IEnumerable<IRollingStock> RollingStocks() => CatalogSeedData.CatalogItems.Rivarossi_HR4298().RollingStocks;
    }
}