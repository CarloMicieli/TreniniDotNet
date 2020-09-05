using System;
using Xunit;

namespace TreniniDotNet.SharedKernel.Slugs
{
    public class ToSeoFriendlyExtensionsTests
    {
        [Fact]
        public void ItShouldCreateASlug()
        {
            var slug = "My test Sample".ToSeoFriendly();
            Assert.Equal("my-test-sample", slug);
        }

        [Fact]
        public void ItShouldCreateASlugWithAMaxLength()
        {
            var slug = "My test Sample".ToSeoFriendly(5);
            Assert.Equal("my", slug);
        }

        [Fact]
        public void ItShouldCreateASlugFromADateTime()
        {
            DateTime date = new DateTime(2019, 2, 14);
            Assert.Equal("2019-02-14", date.ToSeoFriendly());
        }
    }
}