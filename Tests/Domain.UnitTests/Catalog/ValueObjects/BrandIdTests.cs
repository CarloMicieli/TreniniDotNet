using System;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class BrandIdTests
    {
        [Fact]
        public void ItShouldCheckForBrandIdEquality()
        {
            var guid = Guid.NewGuid();

            BrandId id1 = new BrandId(guid);
            BrandId id2 = new BrandId(guid);

            Assert.True(id1 == id2);
            Assert.True(id1.Equals(id2));
        }

        [Fact]
        public void ItShouldCheckForBrandIdInequality()
        {
            BrandId id1 = new BrandId(Guid.NewGuid());
            BrandId id2 = new BrandId(Guid.NewGuid());

            Assert.False(id1 == id2);
            Assert.True(id1 != id2);
            Assert.True(!id1.Equals(id2));
        }
    }
}
