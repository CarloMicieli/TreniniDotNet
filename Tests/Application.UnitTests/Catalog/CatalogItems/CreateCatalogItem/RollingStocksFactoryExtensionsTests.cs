using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaTime.Testing;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public class RollingStocksFactoryExtensionsTests
    {
        private IRollingStocksFactory Factory;

        public RollingStocksFactoryExtensionsTests()
        {
            Factory = new RollingStocksFactory(
                FakeClock.FromUtc(2019, 11, 25),
                FakeGuidSource.NewSource(Guid.NewGuid()));
        }

        [Fact]
        public void RollingStocksFactoryExtensions_FromInput_ThrowsOutOfRangeExceptionWhenRailwayWasNotFound()
        {
            Action act = () =>
                Factory.FromInput(NewRollingStockInput.With(railway: "not found"), RailwaysDictionary());

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void RollingStocksFactoryExtensions_FromInput_ThrowsArgumentExceptionWhenEraValueIsInvalid()
        {
            var input = NewRollingStockInput.With(
                railway: "fs",
                epoch: "",
                category: Category.ElectricLocomotive.ToString());

            Action act = () => Factory.FromInput(input, RailwaysDictionary());

            act.Should().Throw<ArgumentException>()
                .WithMessage("The value is not a valid Epoch (Parameter 'str')");
        }

        [Fact]
        public void RollingStocksFactoryExtensions_FromInput_ThrowsArgumentExceptionWhenCategoryValueIsInvalid()
        {
            var input = NewRollingStockInput.With(
                railway: "fs",
                epoch: "III",
                category: "");

            Action act = () => Factory.FromInput(input, RailwaysDictionary());

            act.Should().Throw<ArgumentException>()
                .WithMessage("The value '' was not a valid constant for Category.");
        }

        [Fact]
        public void RollingStocksFactoryExtensions_FromInput_ShouldParseCategoryAndEra()
        {
            var expectedCategory = Category.ElectricLocomotive;
            var expectedEra = Epoch.III;

            var input = NewRollingStockInput.With(
                railway: "fs",
                epoch: expectedEra.ToString(),
                category: expectedCategory.ToString());

            var rs = Factory.FromInput(input, RailwaysDictionary());
            rs.Category.Should().Be(expectedCategory);
            rs.Epoch.Should().Be(expectedEra);
        }

        [Fact]
        public void RollingStocksFactoryExtensions_FromInput_ShouldSetControlAndDccInterfaceToTheNoneValues()
        {
            var expectedCategory = Category.ElectricLocomotive;
            var expectedEra = Epoch.III;

            var input = NewRollingStockInput.With(
                railway: "fs",
                epoch: expectedEra.ToString(),
                category: expectedCategory.ToString());

            var rs = Factory.FromInput(input, RailwaysDictionary());
            rs.DccInterface.Should().Be(DccInterface.None);
            rs.Control.Should().Be(Control.None);
        }

        [Fact]
        public void RollingStocksFactoryExtensions_FromInput_ShouldThrowArgumentExceptionWhenLengthValuesAreInvalid()
        {
            var input = NewRollingStockInput.With(
                railway: "fs",
                length: new LengthOverBufferInput(-10M, null),
                epoch: "III",
                category: Category.ElectricLocomotive.ToString());

            Action act = () => Factory.FromInput(input, RailwaysDictionary());
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid value for a length, it must be positive. (Parameter 'value')");
        }

        private static IReadOnlyDictionary<Slug, IRailwayInfo> RailwaysDictionary() =>
            CatalogSeedData.Railways.All()
                .ToDictionary(it => it.Slug, it => it.ToRailwayInfo());
    }
}
