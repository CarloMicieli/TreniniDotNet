using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleTests
    {
        [Fact]
        public void Scale_ShouldCheckScalesEquality()
        {
            var halfZero1 = HalfZero();
            var halfZero2 = HalfZero();

            (halfZero1 == halfZero2).Should().BeTrue();
            (halfZero1 != halfZero2).Should().BeFalse();
            (halfZero1.Equals(halfZero2)).Should().BeTrue();
        }

        [Fact]
        public void Scale_ShouldCheckScalesInequality()
        {
            var halfZero = HalfZero();
            var enne = Enne();

            (halfZero != enne).Should().BeTrue();
            (halfZero.Equals(enne)).Should().BeFalse();
            (halfZero == enne).Should().BeFalse();
        }

        [Fact]
        public void Scale_ShouldProduce_StringRepresentations()
        {
            HalfZero().ToString().Should().Be("H0 (1:87)");
        }

        [Fact]
        public void Scale_With_ShouldModifyValues()
        {
            var modifiedH0 = HalfZero()
                .With(description: "Modified description");

            modifiedH0.Should().NotBeNull();
            modifiedH0.Should().NotBeSameAs(HalfZero());
            modifiedH0.Description.Should().Be("Modified description");
        }

        private static Scale HalfZero() => CatalogSeedData.Scales.ScaleH0();

        private static Scale Enne() => CatalogSeedData.Scales.ScaleN();
    }
}
