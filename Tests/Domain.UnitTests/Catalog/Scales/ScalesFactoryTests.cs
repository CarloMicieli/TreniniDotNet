using System.Collections.Immutable;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Lengths;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactoryTests
    {
        private ScalesFactory Factory { get; }

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
            var expectedGauge = ScaleGauge.Of(16.5M, MeasureUnit.Millimeters, TrackGauge.Standard);

            var scale = Factory.CreateScale(
                "Scale H0",
                Ratio.Of(87.0f),
                expectedGauge,
                "Scale Description",
                ImmutableHashSet<ScaleStandard>.Empty,
                100);

            scale.Id.Should().Be(ExpectedScaleId);
            scale.Name.Should().Be("Scale H0");
            scale.Slug.Should().Be(Slug.Of("scale-h0"));
            scale.Ratio.Should().Be(Ratio.Of(87.0M));
            scale.Gauge.Should().Be(expectedGauge);
            scale.Description.Should().Be("Scale Description");
            scale.Weight.Should().Be(100);
            scale.CreatedDate.Should().Be(ExpectedDateTime);
            scale.Version.Should().Be(1);
        }
    }
}
