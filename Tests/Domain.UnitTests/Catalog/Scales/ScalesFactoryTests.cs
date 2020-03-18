using Xunit;
using FluentAssertions;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactoryTests
    {
        private readonly IScalesFactory factory;

        public ScalesFactoryTests()
        {
            factory = new ScalesFactory();
        }

        [Fact]
        public void ScalesFactory_ShouldCreate_NewScale_FromPrimitiveTypes()
        {
            var id = Guid.NewGuid();

            IScale scale = factory.NewScale(id, "name", "slug", 87M, 16.5M, "standard", "notes");

            scale.ScaleId.Should().Be(new ScaleId(id));
            scale.Name.Should().Be("name");
            scale.Slug.Should().Be(Slug.Of("slug"));
            scale.Ratio.Should().Be(Ratio.Of(87.0M));
            scale.Gauge.Should().Be(Gauge.OfMillimiters(16.5f));
            scale.TrackGauge.Should().Be(TrackGauge.Standard);
            scale.Notes.Should().Be("notes");
        }

        [Fact]
        public void ScalesFactory_ShouldThrowAnException_WhenRatioIsNotValid()
        {
            Action act = () => factory.NewScale(Guid.NewGuid(), "name", "slug", -10M, 16.5M, "standard", "notes");
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("ratio value must be positive");
        }

        [Fact]
        public void ScalesFactory_ShouldThrowAnException_WhenGaugeIsNotValid()
        {
            Action act = () => factory.NewScale(Guid.NewGuid(), "name", "slug", 87M, -16.5M, "standard", "notes");
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("gauge value must be positive");
        }

        [Fact]
        public void ScalesFactory_ShouldSetTrackGaugeAsStandard_WhenTrackGaugeIsNotValid()
        {
            IScale scale = factory.NewScale(Guid.NewGuid(), "name", "slug", 87M, 16.5M, "invalid", "notes");
            scale.TrackGauge.Should().Be(TrackGauge.Standard);
        }

        [Fact]
        public void ScalesFactory_ShouldCreateNewScales()
        {
            ScaleId id = ScaleId.NewId();

            IScale scale = factory.NewScale(id, "name", Slug.Of("slug"), Ratio.Of(87.0f), Gauge.OfMillimiters(16.5M), TrackGauge.Standard, "notes");

            scale.ScaleId.Should().Be(id);
            scale.Name.Should().Be("name");
            scale.Slug.Should().Be(Slug.Of("slug"));
            scale.Ratio.Should().Be(Ratio.Of(87.0M));
            scale.Gauge.Should().Be(Gauge.OfMillimiters(16.5f));
            scale.TrackGauge.Should().Be(TrackGauge.Standard);
            scale.Notes.Should().Be("notes");
        }
    }
}
