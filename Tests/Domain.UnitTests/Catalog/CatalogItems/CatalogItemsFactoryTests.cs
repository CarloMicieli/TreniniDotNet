//using Xunit;
//using FluentAssertions;
//using System;
//using System.Collections.Generic;
//using TreniniDotNet.TestHelpers.SeedData.Catalog;
//using TreniniDotNet.Domain.Catalog.Brands;
//using TreniniDotNet.Domain.Catalog.Scales;
//using System.Linq;
//using TreniniDotNet.Common.Uuid;
//using NodaTime;
//using NodaTime.Testing;
//using TreniniDotNet.Domain.Catalog.ValueObjects;
//using TreniniDotNet.Common;
//using System.Collections.Immutable;

//namespace TreniniDotNet.Domain.Catalog.CatalogItems
//{
//    public class CatalogItemsFactoryTests
//    {
//        private readonly ICatalogItemsFactory itemsFactory;

//        public CatalogItemsFactoryTests()
//        {
//            itemsFactory = new CatalogItemsFactory(
//                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)),
//                FakeGuidSource.NewSource(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//        }

//        [Fact]
//        public void CatalogItemsFactory_ShouldCreateCatalogItems_WithASingleRollingStocks()
//        {
//            var success = itemsFactory.NewCatalogItem(
//                Acme(),
//                "60392",
//                H0(),
//                "dc",
//                null,
//                false,
//                "FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.",
//                null, null,
//                RollingStock()
//            );

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.CatalogItemId.Should().Be(new CatalogItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Slug.Should().Be(Slug.Of("acme-60392"));
//                },
//                Fail: errors => Assert.True(false, "should never get here")
//            );
//        }

//        [Fact]
//        public void CatalogItemsFactory_ShouldCreateCatalogItems_WithMoreRollingStocks()
//        {
//            var success = itemsFactory.NewCatalogItem(
//                Rivarossi(),
//                "HR4298",
//                H0(),
//                "dc",
//                null,
//                false,
//                "FS set 2 carrozze a due assi tipo ''Corbellini'' livrea grigio ardesia di 2 cl.",
//                null, null,
//                RollingStocks().ToImmutableList()
//            );

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.CatalogItemId.Should().Be(new CatalogItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Slug.Should().Be(Slug.Of("rivarossi-HR4298"));
//                    succ.RollingStocks.Should().HaveCount(2);
//                },
//                Fail: errors => Assert.True(false, "should never get here")
//            );
//        }

//        [Fact]
//        public void CatalogItemsFactory_ShouldValidateValues()
//        {
//            var success = itemsFactory.NewCatalogItem(
//                Acme(),
//                "", //must be not empty
//                H0(),
//                "--invalid--",
//                "--invalid--",
//                false,
//                "FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori.",
//                null, null,
//                RollingStock()
//            );

//            success.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();
//                    errorsList.Should().HaveCount(3);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("'' is not a valid item number"),
//                        Error.New("'--invalid--' is not a valid power method"),
//                        Error.New("'--invalid--' is not a valid delivery date")
//                    );
//                }
//            );
//        }

//        private static IScale H0() => CatalogSeedData.Scales.ScaleH0();

//        private static IBrand Acme() => CatalogSeedData.Brands.Acme();

//        private static IBrand Rivarossi() => CatalogSeedData.Brands.Rivarossi();

//        private static IRollingStock RollingStock() => CatalogSeedData.CatalogItems.Acme_60392().RollingStocks.First();

//        private static IEnumerable<IRollingStock> RollingStocks() => CatalogSeedData.CatalogItems.Rivarossi_HR4298().RollingStocks;
//    }
//}