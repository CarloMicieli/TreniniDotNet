using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
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

        private static IScale HalfZero()
        {
            return new Scale(ScaleId.NewId(), Slug.Of("h0"), "H0", Ratio.Of(87f), Gauge.OfMillimiters(16.5f), TrackGauge.Standard, null);
        }
    }
}


