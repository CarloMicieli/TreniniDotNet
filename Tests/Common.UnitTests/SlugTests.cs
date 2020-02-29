using Xunit;

namespace TreniniDotNet.Common.Tests
{
    public class SlugTests
    {
        [Fact]
        public void ItShouldCreateASlugFromStringValues()
        {
            var s = Slug.Of("Hello world");
            Assert.Equal("hello-world", s.ToString());
        }

        [Fact]
        public void ItShouldThrowAnExceptionWhenSlugValueIsEmpty()
        {
            Assert.Throws<InvalidSlugException>(() => Slug.Of(string.Empty));
        }

        [Fact]
        public void ItShouldCreateForObjectsThatImplementsIConvertToSlugInterface()
        {
            var v = new MyClass
            {
                V = 42
            };

            var s = Slug.Of(v);
            Assert.Equal("42", s.ToString());
        }

        [Fact]
        public void ItShouldCompareTwoSlugValues()
        {
            var s1 = Slug.Of("HELLO world");
            var s2 = Slug.Of("hello WORLD");
            Assert.Equal(s1, s2);
            Assert.True(s1 == s2);
            Assert.True(s1.Equals(s2));
        }

        [Fact]
        public void ItShouldCreateEmptySlugsAndTheyAreTheSameInstance()
        {
            var s1 = Slug.Empty;
            var s2 = Slug.Empty;
            Assert.True(s1 == s2);
        }

        [Fact]
        public void ItShouldRunTheFunctionToGenerateSlugWhenTheValueIsEmpty()
        {
            var s1 = Slug.Empty;
            var s2 = Slug.Of("my slug");

            var s3 = s1.OrNewIfEmpty(() => Slug.Of("my other slug"));
            var s4 = s2.OrNewIfEmpty(() => Slug.Of("my other slug"));

            Assert.Equal("my-other-slug", s3.ToString());
            Assert.Equal("my-slug", s4.ToString());
        }

        [Fact]
        public void ItShouldCreateANewSlugFromMultipleValues()
        {
            var s1 = Slug.Of("ACME", "12345");
            var s2 = Slug.Of(new MyClass { V = 42 }, new MyClass { V = 84 });

            Assert.Equal("acme-12345", s1.ToString());
            Assert.Equal("42-84", s2.ToString());
        }
    }

    class MyClass : ICanConvertToSlug<MyClass>
    {
        public int V { set; get; }

        public Slug ToSlug()
        {
            return Slug.Of(V.ToString());
        }
    }
}
