using TreniniDotNet.Common;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleTests
    {
        [Fact]
        public void ItShouldCreateANewScale()
        {
            var halfZero = new Scale("H0", Ratio.Of(87f), Gauge.OfMillimiters(16.5f), TrackGauge.Standard, null);
            Assert.Equal("H0 (1:87)", halfZero.ToString());
        }

        [Fact]
        public void ItShouldCreateAScaleIdWhenOneIsNotProvided()
        {
            var halfZero = new Scale("H0", Ratio.Of(87f), Gauge.OfMillimiters(16.5f), TrackGauge.Standard, null);
            Assert.Equal(Slug.Of("h0"), halfZero.Slug);
        }

        [Fact]
        public void ItShouldReturnBrandPropertiesValues()
        {
            var name = "H0";
            var ratio = Ratio.Of(87f);
            var gauge = Gauge.OfMillimiters(16.5f);
            var notes = "The most famous scale";

            var halfZero = new Scale(name, ratio, gauge, TrackGauge.Standard, notes);

            Assert.Equal(name, halfZero.Name);
            Assert.Equal(ratio, halfZero.Ratio);
            Assert.Equal(gauge, halfZero.Gauge);
            Assert.Equal(notes, halfZero.Notes);
        }

        [Fact]
        public void ItShouldCheckScalesEquality()
        {
            var halfZero1 = HalfZero();
            var halfZero2 = HalfZero();

            Assert.True(halfZero1 == halfZero2);
            Assert.True(halfZero2.Equals(halfZero2));
        }

        [Fact]
        public void ItShouldCheckScalesInequality()
        {
            var halfZero = HalfZero();
            var enne = Enne();

            Assert.True(halfZero != enne);
            Assert.False(halfZero.Equals(enne));
        }

        [Fact]
        public void ItShouldThrowArgumentExceptionWhenScaleNameIsBlank()
        {
            Assert.Throws<ArgumentException>(() => new Scale("", Ratio.Of(87f), Gauge.OfMillimiters(1f), TrackGauge.Standard, null));
        }

        private static Scale HalfZero()
        {
            return new Scale("H0", Ratio.Of(87f), Gauge.OfMillimiters(16.5f), TrackGauge.Standard, null);
        }

        private static Scale Enne()
        {
            return new Scale("n", Ratio.Of(160f), Gauge.OfMillimiters(9f), TrackGauge.Standard, null);
        }
    }
}
