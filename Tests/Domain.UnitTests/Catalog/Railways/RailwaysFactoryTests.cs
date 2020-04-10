//using Xunit;
//using FluentAssertions;
//using System;
//using TreniniDotNet.Domain.Catalog.ValueObjects;
//using TreniniDotNet.Common;
//using NodaTime.Testing;
//using NodaTime;

//namespace TreniniDotNet.Domain.Catalog.Railways
//{
//    public class RailwaysFactoryTests
//    {
//        private readonly IRailwaysFactory factory;

//        public RailwaysFactoryTests()
//        {
//            this.factory = new RailwaysFactory(
//                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)));
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldCreateRailways_WithValidation()
//        {
//            Guid id = Guid.NewGuid();
//            var success = factory.NewRailwayV(
//                id,
//                "name",
//                "company name",
//                "DE",
//                new DateTime(1905, 7, 15),
//                null,
//                true
//            );

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.RailwayId.Should().Be(new RailwayId(id));
//                    succ.Name.Should().Be("name");
//                    succ.Status.Should().Be(RailwayStatus.Active);
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldFailToCreateRailways_WithValidationErrorForCountry()
//        {
//            Guid id = Guid.NewGuid();
//            var failure = factory.NewRailwayV(
//                id,
//                "name",
//                "company name",
//                "--",
//                new DateTime(1905, 7, 15),
//                null,
//                true
//            );

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here - it must fail"),
//                Fail: errors =>
//                {
//                    errors.Should().HaveCount(1);

//                    errors.ToList().Should().Contain(
//                        Error.New("'--' is not a valid country code.")
//                    );
//                });
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldFailToCreateRailways_WithPeriodOfActivityValidation()
//        {
//            Guid id = Guid.NewGuid();
//            var failure = factory.NewRailwayV(
//                id,
//                "name",
//                "company name",
//                "de",
//                new DateTime(1905, 7, 15),
//                null,
//                false
//            );

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here - it must fail"),
//                Fail: errors =>
//                {
//                    errors.Should().HaveCount(1);

//                    errors.ToList().Should().Contain(
//                        Error.New("invalid railway: operating until is missing for an inactive railway")
//                    );
//                });
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldFailToCreateRailways_WhenOperatingUntilHappensBeforeOperatingSince()
//        {
//            DateTime dt = new DateTime(1905, 7, 15);
//            Guid id = Guid.NewGuid();
//            var failure = factory.NewRailwayV(
//                id,
//                "name",
//                "company name",
//                "de",
//                dt,
//                dt.AddDays(-1), // until before since
//                false
//            );

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here - it must fail"),
//                Fail: errors =>
//                {
//                    errors.Should().HaveCount(1);

//                    errors.ToList().Should().Contain(
//                        Error.New("invalid railway: operating since > operating until")
//                    );
//                });
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldFailToCreateRailways_WhenNameIsEmpty()
//        {
//            DateTime dt = new DateTime(1905, 7, 15);
//            Guid id = Guid.NewGuid();
//            var failure = factory.NewRailwayV(
//                id,
//                "    ",
//                "company name",
//                "de",
//                dt,
//                dt.AddDays(1),
//                false
//            );

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here - it must fail"),
//                Fail: errors =>
//                {
//                    errors.Should().HaveCount(1);

//                    errors.ToList().Should().Contain(
//                        Error.New("invalid railway: name cannot be empty")
//                    );
//                });
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldCreateRailways_FromPrimitives()
//        {
//            Guid id = Guid.NewGuid();
//            IRailway railway = factory.NewRailway(
//                "name",
//                "company name",
//                "DE",
//                new DateTime(1905, 7, 15),
//                null,
//                RailwayStatus.Active
//            );

//            railway.Name.Should().Be("name");
//            railway.Slug.Should().Be(Slug.Of("name"));
//            railway.CompanyName.Should().Be("company name");
//            railway.Country.Should().Be("DE");
//            railway.OperatingSince.Should().Be(new DateTime(1905, 7, 15));
//            railway.OperatingUntil.Should().BeNull();
//            railway.Status.Should().Be(RailwayStatus.Active);
//        }

//        [Fact]
//        public void RailwaysFactory_ShouldCreateRailways_FromDatabase()
//        {
//            DateTime now = DateTime.UtcNow;
//            Guid id = Guid.NewGuid();
//            IRailway railway = factory.NewRailway(
//                 id,
//                "name",
//                "slug",
//                "company name",
//                "DE",
//                new DateTime(1905, 7, 15),
//                null,
//                false,
//                now,
//                2
//            );

//            railway.Name.Should().Be("name");
//            railway.Slug.Should().Be(Slug.Of("slug"));
//            railway.CompanyName.Should().Be("company name");
//            railway.Country.Should().Be("DE");
//            railway.OperatingSince.Should().Be(new DateTime(1905, 7, 15));
//            railway.OperatingUntil.Should().BeNull();
//            railway.Status.Should().Be(RailwayStatus.Inactive);
//            railway.Version.Should().Be(2);
//            railway.CreatedAt.Should().Be(now);
//        }


//        [Fact]
//        public void RailwaysFactory_ShouldCreateRailways_FromDomainObjects()
//        {
//            RailwayId id = new RailwayId(Guid.NewGuid());
//            IRailway railway = factory.NewRailway(
//                id,
//                "name",
//                Slug.Of("slug"),
//                "company name",
//                "DE",
//                new DateTime(1905, 7, 15),
//                null,
//                RailwayStatus.Active
//            );

//            railway.Name.Should().Be("name");
//            railway.Slug.Should().Be(Slug.Of("slug"));
//            railway.CompanyName.Should().Be("company name");
//            railway.Country.Should().Be("DE");
//            railway.OperatingSince.Should().Be(new DateTime(1905, 7, 15));
//            railway.OperatingUntil.Should().BeNull();
//            railway.Status.Should().Be(RailwayStatus.Active);
//        }
//    }
//}