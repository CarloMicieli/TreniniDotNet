using Xunit;
using FluentAssertions;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwaysFactoryTests
    {
        private readonly IRailwaysFactory factory;

        public RailwaysFactoryTests()
        {
            this.factory = new RailwaysFactory();
        }

        [Fact]
        public void RailwaysFactory_ShouldCreateRailways_FromPrimitives()
        {
            Guid id = Guid.NewGuid();
            IRailway railway = factory.NewRailway(
                "name",
                "company name",
                "DE",
                new DateTime(1905, 7, 15),
                null,
                RailwayStatus.Active
            );

            railway.Name.Should().Be("name");
            railway.Slug.Should().Be(Slug.Of("name"));
            railway.CompanyName.Should().Be("company name");
            railway.Country.Should().Be("DE");
            railway.OperatingSince.Should().Be(new DateTime(1905, 7, 15));
            railway.OperatingUntil.Should().BeNull();
            railway.Status.Should().Be(RailwayStatus.Active);
        }

        [Fact]
        public void RailwaysFactory_ShouldCreateRailways_FromDatabase()
        {
            DateTime now = DateTime.UtcNow;
            Guid id = Guid.NewGuid();
            IRailway railway = factory.NewRailway(
                 id,
                "name",
                "slug",
                "company name",
                "DE",
                new DateTime(1905, 7, 15),
                null,
                false,
                now,
                2
            );

            railway.Name.Should().Be("name");
            railway.Slug.Should().Be(Slug.Of("slug"));
            railway.CompanyName.Should().Be("company name");
            railway.Country.Should().Be("DE");
            railway.OperatingSince.Should().Be(new DateTime(1905, 7, 15));
            railway.OperatingUntil.Should().BeNull();
            railway.Status.Should().Be(RailwayStatus.Inactive);
            railway.Version.Should().Be(2);
            railway.CreatedAt.Should().Be(now);
        }


        [Fact]
        public void RailwaysFactory_ShouldCreateRailways_FromDomainObjects()
        {
            RailwayId id = new RailwayId(Guid.NewGuid());
            IRailway railway = factory.NewRailway(
                id,
                "name",
                Slug.Of("slug"),
                "company name",
                "DE",
                new DateTime(1905, 7, 15),
                null,
                RailwayStatus.Active
            );

            railway.Name.Should().Be("name");
            railway.Slug.Should().Be(Slug.Of("slug"));
            railway.CompanyName.Should().Be("company name");
            railway.Country.Should().Be("DE");
            railway.OperatingSince.Should().Be(new DateTime(1905, 7, 15));
            railway.OperatingUntil.Should().BeNull();
            railway.Status.Should().Be(RailwayStatus.Active);
        }
    }
}