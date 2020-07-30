using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionStatsTests
    {
        [Fact]
        public void CollectionStats_ShouldCalculateCollectionStatistics_ForEmptyCollections()
        {
            var collection = CollectingSeedData.Collections
                .New()
                .Owner(new Owner("George"))
                .Build();

            var stats = CollectionStats.FromCollection(collection);

            stats.TotalValue.Should().Be(Price.Euro(0M));
            stats.Owner.Should().Be(collection.Owner);
            stats.Id.Should().Be(collection.Id);
            stats.CategoriesByYear.Should().HaveCount(0);
        }

        [Fact]
        public void CollectionStats_ShouldCalculateCollectionStatistics()
        {
            var collection = CollectingSeedData.Collections.RocketCollection();

            var stats = CollectionStats.FromCollection(collection);

            stats.TotalValue.Should().Be(Price.Euro(1104.90M));
            stats.Owner.Should().Be(collection.Owner);
            stats.Id.Should().Be(collection.Id);
            stats.CategoriesByYear.Should().HaveCount(3);
        }
    }
}
