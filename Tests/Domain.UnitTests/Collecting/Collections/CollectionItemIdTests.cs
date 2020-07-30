using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionItemIdTests
    {
        [Fact]
        public void CollectionItemId_ShouldCreateNewValues()
        {
            var id = CollectionItemId.NewId();
            id.Should().NotBeNull();
        }

        [Fact]
        public void CollectionItemId_ShouldConvertToGuidAndBack()
        {
            var cId = CollectionItemId.NewId();

            Guid id = cId;
            CollectionItemId cId2 = (CollectionItemId)id;

            cId.Should().Be(cId2);
        }

        [Fact]
        public void CollectionItemId_ShouldCheckForEquality()
        {
            var id1 = new CollectionItemId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));
            var id2 = new CollectionItemId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));

            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
            id1.Equals(id2).Should().BeTrue();
        }
    }
}
