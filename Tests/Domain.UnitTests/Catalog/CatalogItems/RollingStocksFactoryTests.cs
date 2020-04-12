//using Xunit;
//using FluentAssertions;
//using NodaTime.Testing;
//using NodaTime;
//using TreniniDotNet.Common.Uuid;
//using System;
//using TreniniDotNet.Domain.Catalog.Railways;
//using TreniniDotNet.TestHelpers.SeedData.Catalog;
//using TreniniDotNet.Domain.Catalog.ValueObjects;
//using TreniniDotNet.Common;
//using System.Linq;
//using TreniniDotNet.Common.Lengths;

//namespace TreniniDotNet.Domain.Catalog.CatalogItems
//{
//    public class RollingStocksFactoryTests
//    {
//        private readonly IRollingStocksFactory factory;

//        public RollingStocksFactoryTests()
//        {
//            factory = new RollingStocksFactory(
//                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)),
//                FakeGuidSource.NewSource(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"))
//            );
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldCreateLocomotives_WithValidation()
//        {
//            var success = factory.NewLocomotive(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.ElectricLocomotive.ToString(),
//                210M,
//                "E 656",
//                "E 656 210",
//                DccInterface.Nem652.ToString(),
//                Control.DccReady.ToString());

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.RollingStockId.Should().Be(new RollingStockId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Era.Should().Be(Era.IV);
//                    succ.Category.Should().Be(Category.ElectricLocomotive);
//                    succ.Railway.Should().Be(Fs());
//                    succ.Length.Should().Be(Length.OfMillimeters(210));
//                    succ.ClassName.Should().Be("E 656");
//                    succ.RoadNumber.Should().Be("E 656 210");
//                    succ.DccInterface.Should().Be(DccInterface.Nem652);
//                    succ.Control.Should().Be(Control.DccReady);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact(Skip = "Fix me")]
//        public void RollingStocksFactory_ShouldCreateLocomotives_WhenOptionalValuesAreNull()
//        {
//            var success = factory.NewLocomotive(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.ElectricLocomotive.ToString(),
//                null,
//                "E 656",
//                "E 656 210",
//                null,
//                null);

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.RollingStockId.Should().Be(new RollingStockId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Era.Should().Be(Era.IV);
//                    succ.Category.Should().Be(Category.ElectricLocomotive);
//                    succ.Railway.Should().Be(Fs());
//                    succ.ClassName.Should().Be("E 656");
//                    succ.RoadNumber.Should().Be("E 656 210");
//                    succ.DccInterface.Should().Be(DccInterface.None);
//                    succ.Control.Should().Be(Control.None);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldFailToCreateLocomotives_WhenValidationFails()
//        {
//            var failure = factory.NewLocomotive(
//                Fs(),
//                "--invalid era--",
//                "--invalid category--",
//                -210M,
//                "E 656",
//                "E 656 210",
//                "--invalid dcc--",
//                "--invalid control--");

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();
//                    errorsList.Should().HaveCount(5);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("'--invalid era--' is not a valid era value"),
//                        Error.New("'--invalid category--' is not a valid category"),
//                        Error.New($"{-210M} is not a length value"),
//                        Error.New("'--invalid control--' is not a valid control value"),
//                        Error.New("'--invalid dcc--' is not a valid dcc interface")
//                    );
//                }
//            );
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldFailToCreateLocomotives_WhenCategoryIsNotValid()
//        {
//            var failure = factory.NewLocomotive(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.PassengerCar.ToString(),
//                null,
//                "E 656",
//                "E 656 210",
//                null,
//                null);

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();
//                    errorsList.Should().HaveCount(1);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("'PassengerCar' is not a valid category for a locomotive")
//                    );
//                }
//            );
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldCreateTrains_WithValidation()
//        {
//            var success = factory.NewTrain(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.ElectricMultipleUnit.ToString(),
//                1000M,
//                "Ale 601",
//                "Ale 601 1096",
//                DccInterface.Nem652.ToString(),
//                Control.DccReady.ToString());

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.RollingStockId.Should().Be(new RollingStockId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Era.Should().Be(Era.IV);
//                    succ.Category.Should().Be(Category.ElectricMultipleUnit);
//                    succ.Railway.Should().Be(Fs());
//                    succ.Length.Should().Be(Length.OfMillimeters(1000));
//                    succ.ClassName.Should().Be("Ale 601");
//                    succ.RoadNumber.Should().Be("Ale 601 1096");
//                    succ.DccInterface.Should().Be(DccInterface.Nem652);
//                    succ.Control.Should().Be(Control.DccReady);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact(Skip = "Fix me")]
//        public void RollingStocksFactory_ShouldCreateTrains_WhenOptionalValuesAreNull()
//        {
//            var success = factory.NewTrain(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.ElectricMultipleUnit.ToString(),
//                null,
//                "Ale 601",
//                "Ale 601 1096",
//                null,
//                null);

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.RollingStockId.Should().Be(new RollingStockId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Era.Should().Be(Era.IV);
//                    succ.Category.Should().Be(Category.ElectricMultipleUnit);
//                    succ.Railway.Should().Be(Fs());
//                    succ.ClassName.Should().Be("Ale 601");
//                    succ.RoadNumber.Should().Be("Ale 601 1096");
//                    succ.DccInterface.Should().Be(DccInterface.None);
//                    succ.Control.Should().Be(Control.None);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldFailToCreateTrains_WhenValidationFails()
//        {
//            var failure = factory.NewTrain(
//                Fs(),
//                "--invalid era--",
//                "--invalid category--",
//                -210M,
//                "E 656",
//                "E 656 210",
//                "--invalid dcc--",
//                "--invalid control--");

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();
//                    errorsList.Should().HaveCount(5);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("'--invalid era--' is not a valid era value"),
//                        Error.New("'--invalid category--' is not a valid category"),
//                        Error.New($"{-210M} is not a length value"),
//                        Error.New("'--invalid control--' is not a valid control value"),
//                        Error.New("'--invalid dcc--' is not a valid dcc interface")
//                    );
//                }
//            );
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldFailToCreateTrains_WhenCategoryIsNotValid()
//        {
//            var failure = factory.NewTrain(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.PassengerCar.ToString(),
//                null,
//                "E 656",
//                "E 656 210",
//                null,
//                null);

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();
//                    errorsList.Should().HaveCount(1);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("'PassengerCar' is not a valid category for a train")
//                    );
//                }
//            );
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldCreateNewRollingStocks_WithValidation()
//        {
//            var success = factory.NewRollingStock(
//                Fs(),
//                Era.IV.ToString().ToUpper(),
//                Category.PassengerCar.ToString(),
//                303M,
//                "UIC Tipo-Z");

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.RollingStockId.Should().Be(new RollingStockId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af")));
//                    succ.Era.Should().Be(Era.IV);
//                    succ.Category.Should().Be(Category.PassengerCar);
//                    succ.Railway.Should().Be(Fs());
//                    succ.Length.Should().Be(Length.OfMillimeters(303));
//                    succ.ClassName.Should().BeNull();
//                    succ.RoadNumber.Should().BeNull();
//                    succ.TypeName.Should().Be("UIC Tipo-Z");
//                    succ.DccInterface.Should().Be(DccInterface.None);
//                    succ.Control.Should().Be(Control.None);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldFailToCreateRollingStocks_WhenValidationFails()
//        {
//            var failure = factory.NewRollingStock(
//                Fs(),
//                "--invalid era--",
//                "--invalid category--",
//                -210M,
//                "UIC Tipo-Z");

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();
//                    errorsList.Should().HaveCount(3);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("'--invalid era--' is not a valid era value"),
//                        Error.New("'--invalid category--' is not a valid category"),
//                        Error.New($"{-210M} is not a length value")
//                    );
//                }
//            );
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldHydrateLocomotives()
//        {
//            var it = Locomotive();
//            var result = factory.HydrateRollingStock(
//                it.RollingStockId.ToGuid(),
//                it.Railway, it.Era.ToString(), it.Category.ToString(),
//                it.Length.Value,
//                it.ClassName, it.RoadNumber, it.TypeName,
//                it.DccInterface.ToString(), it.Control.ToString());

//            result.Match(
//                Succ: rs =>
//                {
//                    rs.Should().Be(it);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact]
//        public void RollingStocksFactory_ShouldHydrateARollingStockWithoutMotor()
//        {
//            var it = PassengerCar();
//            var result = factory.HydrateRollingStock(
//                it.RollingStockId.ToGuid(),
//                it.Railway, it.Era.ToString(), it.Category.ToString(),
//                it.Length.Value,
//                it.ClassName, it.RoadNumber, it.TypeName,
//                it.DccInterface.ToString(), it.Control.ToString());

//            result.Match(
//                Succ: rs =>
//                {
//                    rs.Should().Be(it);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        private static IRailwayInfo Fs() => CatalogSeedData.Railways.Fs();

//        private static IRollingStock Locomotive() =>
//            CatalogSeedData.CatalogItems.Acme_60392().RollingStocks.First();

//        private static IRollingStock PassengerCar() =>
//            CatalogSeedData.CatalogItems.Rivarossi_HR4298().RollingStocks.First();
//    }
//}