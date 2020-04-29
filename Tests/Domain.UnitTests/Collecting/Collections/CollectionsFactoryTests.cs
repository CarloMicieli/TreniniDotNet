using System;
using FluentAssertions;
using NodaMoney;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionsFactoryTests
    {
        private ICollectionsFactory Factory { get; }

        public CollectionsFactoryTests()
        {
            Factory = new CollectionsFactory(
                FakeClock.FromUtc(1988, 11, 25),
                FakeGuidSource.NewSource(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
        }

        [Fact]
        public void CollectionsFactory_ShouldCreateNewCollections()
        {
            var collection = Factory.NewCollection("George");

            collection.Should().NotBeNull();
            collection.CollectionId.Should().Be(new CollectionId(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
            collection.Owner.Should().Be(new Owner("George"));
            collection.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 0, 0));
            collection.Version.Should().Be(1);
        }

        [Fact]
        public void CollectionsFactory_ShouldCreateNewCollectionItems()
        {
            var collectionItem = Factory.NewCollectionItem(
                CatalogRef.Of(Guid.NewGuid(), "acme-123456"),
                null,
                Condition.New,
                Money.Euro(450),
                new LocalDate(2020, 11, 25),
                null,
                "My Notes");

            collectionItem.Should().NotBeNull();
            collectionItem.ItemId.Should().Be(new CollectionItemId(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
        }
    }
}