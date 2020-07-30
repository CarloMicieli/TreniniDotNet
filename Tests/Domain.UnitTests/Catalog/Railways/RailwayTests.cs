using System;
using FluentAssertions;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayTests
    {
        [Fact]
        public void Railway_Equals_ShouldCheckForRailwaysEquality()
        {
            var fs1 = CatalogSeedData.Railways.New()
                .Id(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313"))
                .Name("FS")
                .CompanyName("Ferrovie dello stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)))
                .Build();

            var fs2 = CatalogSeedData.Railways.New()
                .Id(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313"))
                .Name("FS")
                .CompanyName("Ferrovie dello stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)))
                .Build();

            (fs1 == fs2).Should().BeTrue();
            fs1.Equals(fs2).Should().BeTrue();
            (fs1 != fs2).Should().BeFalse();
        }

        [Fact]
        public void Railway_Equals_ShouldCheckForRailwaysInequality()
        {
            var fs1 = CatalogSeedData.Railways.New()
                .Id(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313"))
                .Name("FS")
                .CompanyName("Ferrovie dello stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)))
                .Build();

            var dieBahn = CatalogSeedData.Railways.New()
                .Id(new Guid("f12a3c5b-21f0-4d96-baf0-7bbf67e85e93"))
                .Name("DB")
                .CompanyName("Die Bahn AG")
                .Country(Country.Of("DE"))
                .Build();

            (fs1 == dieBahn).Should().BeFalse();
            fs1.Equals(dieBahn).Should().BeFalse();
            (fs1 != dieBahn).Should().BeTrue();
        }

        [Fact]
        public void Railway_With_ShouldModifyRailwayValues()
        {
            var modifiedFs = CatalogSeedData.Railways.Fs()
                .With(companyName: "FS 2");

            modifiedFs.Should().NotBeNull();
            modifiedFs.Should().NotBeSameAs(CatalogSeedData.Railways.Fs());
            modifiedFs.CompanyName.Should().Be("FS 2");
        }
    }
}
