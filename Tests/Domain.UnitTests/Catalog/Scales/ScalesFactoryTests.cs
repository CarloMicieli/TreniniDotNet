using System.Collections.Immutable;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactoryTests
    {
        private IScalesFactory Factory { get; }

        private ScaleId ExpectedScaleId = ScaleId.NewId();

        private Instant ExpectedDateTime = Instant.FromUtc(1988, 11, 25, 0, 0);

        public ScalesFactoryTests()
        {
            Factory = new ScalesFactory(
                new FakeClock(ExpectedDateTime),
                FakeGuidSource.NewSource(ExpectedScaleId.ToGuid()));
        }

        [Fact]
        public void ScalesFactory_CreateNewScale_ShouldCreateNewScaleValues()
        {
            var ExpectedGauge = ScaleGauge.Of(16.5M, MeasureUnit.Millimeters, TrackGauge.Standard);

            IScale scale = Factory.CreateNewScale(
                "Scale H0",
                Ratio.Of(87.0f),
                ExpectedGauge,
                "Scale Description",
                ImmutableHashSet<ScaleStandard>.Empty,
                100);

            scale.Id.Should().Be(ExpectedScaleId);
            scale.Name.Should().Be("Scale H0");
            scale.Slug.Should().Be(Slug.Of("scale-h0"));
            scale.Ratio.Should().Be(Ratio.Of(87.0M));
            scale.Gauge.Should().Be(ExpectedGauge);
            scale.Description.Should().Be("Scale Description");
            scale.Weight.Should().Be(100);
            scale.CreatedDate.Should().Be(ExpectedDateTime);
            scale.Version.Should().Be(1);
        }

        [Fact]
        public void ScalesFactory_NewScale_ShouldCreateScaleValues()
        {
            var ExpectedGauge = ScaleGauge.Of(16.5M, MeasureUnit.Millimeters, TrackGauge.Standard);

            IScale scale = Factory.NewScale(
                ExpectedScaleId.ToGuid(),
                "Scale H0", "scale-h0",
                87M,
                16.5M, 0.65M, TrackGauge.Standard.ToString(),
                "Scale Description",
                100,
                ExpectedDateTime.ToDateTimeUtc(),
                ExpectedDateTime.ToDateTimeUtc(),
                3);

            scale.Id.Should().Be(ExpectedScaleId);
            scale.Name.Should().Be("Scale H0");
            scale.Slug.Should().Be(Slug.Of("scale-h0"));
            scale.Ratio.Should().Be(Ratio.Of(87.0M));
            scale.Gauge.Should().Be(ExpectedGauge);
            scale.Description.Should().Be("Scale Description");
            scale.Weight.Should().Be(100);
            scale.CreatedDate.Should().Be(ExpectedDateTime);
            scale.ModifiedDate.Should().Be(ExpectedDateTime);
            scale.Version.Should().Be(3);
        }

        [Fact]
        public void ScalesFactory_UpdateScale_ShouldCreateScaleValuesApplyChanges()
        {
            var ExpectedGauge = ScaleGauge.Of(16.5M, MeasureUnit.Millimeters, TrackGauge.Standard);
            IScale scaleH0 = CatalogSeedData.Scales.ScaleH0();

            IScale scale = Factory.UpdateScale(scaleH0,
                null,
                null,
                ExpectedGauge,
                "Updated description",
                null,
                100);

            scale.Id.Should().Be(scaleH0.Id);
            scale.Name.Should().Be("H0");
            scale.Slug.Should().Be(Slug.Of("h0"));
            scale.Ratio.Should().Be(Ratio.Of(87.0M));
            scale.Gauge.Should().Be(ExpectedGauge);
            scale.Description.Should().Be("Updated description");
            scale.Weight.Should().Be(100);
            scale.ModifiedDate.Should().Be(ExpectedDateTime);
            scale.Version.Should().Be(2);
        }
    }
}
