using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleTests
    {
        [Fact]
        public void Scales_ShouldCheckScalesEquality()
        {
            var halfZero1 = HalfZero();
            var halfZero2 = HalfZero();

            //(halfZero1 == halfZero2).Should().BeTrue();
            (halfZero1.Equals(halfZero2)).Should().BeTrue();
        }

        [Fact]
        public void Scales_ShouldCheckScalesInequality()
        {
            var halfZero = HalfZero();
            var enne = Enne();

            (halfZero != enne).Should().BeTrue();
            (halfZero.Equals(enne)).Should().BeFalse();
        }

        [Fact]
        public void Scales_ShouldProduce_StringRepresentations()
        {
            HalfZero().ToString().Should().Be("H0 (1:87)");
            HalfZero().ToLabel().Should().Be("H0 (1:87)");
        }

        private static IScale HalfZero() => CatalogSeedData.Scales.ScaleH0();

        private static IScale Enne() => CatalogSeedData.Scales.ScaleN();
    }
}
