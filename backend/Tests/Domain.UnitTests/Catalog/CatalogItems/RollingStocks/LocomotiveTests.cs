using System.Linq;
using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public class LocomotiveTests
    {
        private Locomotive Locomotive { get; }

        public LocomotiveTests()
        {
            Locomotive = CatalogSeedData.CatalogItems.NewAcme60392().RollingStocks.First() as Locomotive;
        }

        [Fact]
        public void Locomotive_With_ShouldCreateModifiedValue()
        {
            var modified = Locomotive.With(epoch: Epoch.I);
            modified.Epoch.Should().Be(Epoch.I);
        }
    }
}