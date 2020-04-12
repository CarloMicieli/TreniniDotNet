//using Xunit;
//using FluentAssertions;
//using System;
//using TreniniDotNet.Domain.Catalog.ValueObjects;
//using TreniniDotNet.Common;
//using NodaTime.Testing;
//using NodaTime;

//namespace TreniniDotNet.Domain.Catalog.Scales
//{
//    public class ScalesFactoryTests
//    {
//        private readonly IScalesFactory factory;

//        public ScalesFactoryTests()
//        {
//            factory = new ScalesFactory(
//                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)));
//        }

//        [Fact]
//        public void ScalesFactory_ShouldCreateNewScales_WithValidation()
//        {
//            var id = Guid.NewGuid();

//            var success = factory.NewScaleV(id, "name", 87M, 16.5M, "standard", "notes");

//            success.Match(
//                Succ: succ =>
//                {
//                    succ.Name.Should().Be("name");
//                    succ.Ratio.ToDecimal().Should().Be(87M);
//                    succ.Gauge.ToDecimal(MeasureUnit.Millimeters).Should().Be(16.5M);
//                    succ.Version.Should().Be(1);
//                    succ.CreatedAt.Should().Be(Instant.FromUtc(1988, 11, 25, 0, 0).ToDateTimeUtc());
//                },
//                Fail: errors => Assert.True(false, "should never get here"));
//        }

//        [Fact]
//        public void ScalesFactory_ShouldFailToCreateNewScales_WithValidationErrors()
//        {
//            var id = Guid.NewGuid();

//            var failure = factory.NewScaleV(id, "", -87M, -16.5M, "--invalid--", "notes");

//            failure.Match(
//                Succ: succ => Assert.True(false, "should never get here"),
//                Fail: errors =>
//                {
//                    var errorsList = errors.ToList();

//                    errorsList.Should().HaveCount(4);
//                    errorsList.Should().ContainInOrder(
//                        Error.New("invalid scale: name cannot be empty"),
//                        Error.New("ratio value must be positive"),
//                        Error.New("gauge value must be positive"),
//                        Error.New("'--invalid--' is not a valid track gauge")
//                    );
//                });
//        }

//        [Fact]
//        public void ScalesFactory_ShouldCreate_NewScale_FromPrimitiveTypes()
//        {
//            var id = Guid.NewGuid();

//            IScale scale = factory.NewScale(id, "name", "slug", 87M, 16.5M, "standard", "notes");

//            scale.ScaleId.Should().Be(new ScaleId(id));
//            scale.Name.Should().Be("name");
//            scale.Slug.Should().Be(Slug.Of("slug"));
//            scale.Ratio.Should().Be(Ratio.Of(87.0M));
//            scale.Gauge.Should().Be(Gauge.OfMillimiters(16.5f));
//            scale.TrackGauge.Should().Be(TrackGauge.Standard);
//            scale.Notes.Should().Be("notes");
//        }

//        [Fact]
//        public void ScalesFactory_ShouldThrowAnException_WhenRatioIsNotValid()
//        {
//            Action act = () => factory.NewScale(Guid.NewGuid(), "name", "slug", -10M, 16.5M, "standard", "notes");
//            act.Should()
//                .Throw<ArgumentException>()
//                .WithMessage("ratio value must be positive");
//        }

//        [Fact]
//        public void ScalesFactory_ShouldThrowAnException_WhenGaugeIsNotValid()
//        {
//            Action act = () => factory.NewScale(Guid.NewGuid(), "name", "slug", 87M, -16.5M, "standard", "notes");
//            act.Should()
//                .Throw<ArgumentException>()
//                .WithMessage("gauge value must be positive");
//        }

//        [Fact]
//        public void ScalesFactory_ShouldSetTrackGaugeAsStandard_WhenTrackGaugeIsNotValid()
//        {
//            IScale scale = factory.NewScale(Guid.NewGuid(), "name", "slug", 87M, 16.5M, "invalid", "notes");
//            scale.TrackGauge.Should().Be(TrackGauge.Standard);
//        }

//        [Fact]
//        public void ScalesFactory_ShouldCreateNewScales()
//        {
//            ScaleId id = ScaleId.NewId();

//            IScale scale = factory.NewScale(id, "name", Slug.Of("slug"), Ratio.Of(87.0f), Gauge.OfMillimiters(16.5M), TrackGauge.Standard, "notes");

//            scale.ScaleId.Should().Be(id);
//            scale.Name.Should().Be("name");
//            scale.Slug.Should().Be(Slug.Of("slug"));
//            scale.Ratio.Should().Be(Ratio.Of(87.0M));
//            scale.Gauge.Should().Be(Gauge.OfMillimiters(16.5f));
//            scale.TrackGauge.Should().Be(TrackGauge.Standard);
//            scale.Notes.Should().Be("notes");
//        }
//    }
//}
