using System;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collecting.Shared;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionsFactoryTests
    {
        private CollectionsFactory Factory { get; }

        public CollectionsFactoryTests()
        {
            Factory = new CollectionsFactory(
                FakeClock.FromUtc(1988, 11, 25),
                FakeGuidSource.NewSource(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
        }

        [Fact]
        public void CollectionsFactory_ShouldCreateNewCollections()
        {
            var collection = Factory.CreateCollection(new Owner("George"), "notes");

            collection.Should().NotBeNull();
            collection.Id.Should().Be(new CollectionId(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
            collection.Owner.Should().Be(new Owner("George"));
            collection.Notes.Should().Be("notes");
            collection.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 0, 0));
            collection.Version.Should().Be(1);
        }
    }
}
