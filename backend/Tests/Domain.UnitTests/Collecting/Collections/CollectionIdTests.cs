using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionIdTests
    {
        [Fact]
        public void CollectionId_ShouldCreateNewValues()
        {
            var id = CollectionId.NewId();
            id.Should().NotBeNull();
        }

        [Fact]
        public void CollectionId_ShouldConvertToGuidAndBack()
        {
            var cId = CollectionId.NewId();

            Guid id = cId;
            CollectionId cId2 = (CollectionId)id;

            cId.Should().Be(cId2);
        }

        [Fact]
        public void CollectionId_ShouldCheckForEquality()
        {
            var id1 = new CollectionId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));
            var id2 = new CollectionId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));

            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
            id1.Equals(id2).Should().BeTrue();
        }
    }
}
