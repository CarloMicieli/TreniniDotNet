using NodaTime;
using NodaMoney;
using NodaTime.Testing;
using System;
using Xunit;
using FluentAssertions;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
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
            collection.Owner.Should().Be("George");
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