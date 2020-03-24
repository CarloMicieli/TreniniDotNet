using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleInfoTests
    {
        [Fact]
        public void ItShouldExtractScaleInfo_FromScales()
        {
            IScaleInfo info = HalfZero().ToScaleInfo();
            Assert.NotNull(info);
            Assert.Equal(HalfZero().Name, info.Name);
            Assert.Equal(HalfZero().Ratio, info.Ratio);
            Assert.Equal(HalfZero().Slug, info.Slug);
        }

        [Fact]
        public void ItShouldCrateLabelForScaleInfos()
        {
            IScaleInfo info = HalfZero().ToScaleInfo();
            Assert.NotNull(info);
            Assert.Equal("H0 (1:87)", info.ToLabel());
        }

        private static IScale HalfZero() => CatalogSeedData.Scales.ScaleH0();
    }
}


