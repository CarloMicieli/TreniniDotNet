using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.SharedKernel.Slugs
{
    public class SlugTests
    {
        [Fact]
        public void Slug_Of_ShouldCreateASlugFromStringValues()
        {
            var s = Slug.Of("Hello world");
            s.ToString().Should().Be("hello-world");
        }

        [Fact]
        public void Slug_Of_ShouldThrowAnExceptionWhenSlugValueIsEmpty()
        {
            Action act = () => Slug.Of(string.Empty);
            act.Should().Throw<InvalidSlugException>();
        }

        [Fact]
        public void Slug_Of_ShouldCreateForObjectsThatImplementsIConvertToSlugInterface()
        {
            var v = new MyClass
            {
                V = 42
            };

            var s = Slug.Of(v);
            s.ToString().Should().Be("42");
        }

        [Fact]
        public void Slug_ItShouldCompareTwoSlugValues()
        {
            var s1 = Slug.Of("HELLO world");
            var s2 = Slug.Of("hello WORLD");

            s1.Equals(s2).Should().BeTrue();
            (s1 == s2).Should().BeTrue();
            (s1 != s2).Should().BeFalse();
        }

        [Fact]
        public void Slug_ShouldCreateEmptySlugsAndTheyAreTheSameInstance()
        {
            var s1 = Slug.Empty;
            var s2 = Slug.Empty;
            (s1 == s2).Should().BeTrue();
        }

        [Fact]
        public void Slug_ShouldCreateANewSlugFromMultipleValues()
        {
            var s1 = Slug.Of("ACME", "12345");
            var s2 = Slug.Of(new MyClass { V = 42 }, new MyClass { V = 84 });

            s1.ToString().Should().Be("acme-12345");
            s2.ToString().Should().Be("42-84");
        }

        [Fact]
        public void Slug_Combine_ShouldCombineASlugWithAValue()
        {
            var slug = Slug.Of("my slug");
            var otherValue = new MyClass { V = 42 };

            slug.CombineWith(otherValue).Value.Should().Be("my-slug-42");
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