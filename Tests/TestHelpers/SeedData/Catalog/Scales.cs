using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Scales
    {
        private static readonly IScalesFactory factory = new ScalesFactory();

        private readonly IScale _scaleH0;
        private readonly IScale _scaleH0m;
        private readonly IScale _scaleH0e;
        private readonly IScale _scaleTT;
        private readonly IScale _scale1;
        private readonly IScale _scale0;
        private readonly IScale _scaleN;
        private readonly IScale _scaleZ;

        private readonly IList<IScale> _all;

        internal Scales()
        {
            #region [ Init data ]
            _scaleH0 = factory.NewScale(
                new ScaleId(new Guid("7edfb586-218c-4997-8820-f61d3a81ce66")),
                "H0",
                Slug.Of("H0"),
                Ratio.Of(87.0M),
                Gauge.OfMillimiters(16.5M),
                TrackGauge.Standard,
                null);

            _scaleH0m = factory.NewScale(
               new ScaleId(new Guid("0dd13f9d-d730-41bb-b4e9-33218ea14fbc")),
               "H0m",
               Slug.Of("H0m"),
               Ratio.Of(87.0M),
               Gauge.OfMillimiters(12.0M),
               TrackGauge.Narrow,
               null);

            _scaleH0e = factory.NewScale(
                new ScaleId(new Guid("b5f2f033-a947-4b86-9d9e-52d7c1903ce0")),
                "H0e",
                Slug.Of("H0e"),
                Ratio.Of(87.0M),
                Gauge.OfMillimiters(9.0M),
                TrackGauge.Narrow,
                null);

            _scaleTT = factory.NewScale(
               new ScaleId(new Guid("374f5bb7-e7d1-4995-aa34-072b6b6500f9")),
               "TT",
               Slug.Of("TT"),
               Ratio.Of(120.0M),
               Gauge.OfMillimiters(12.0M),
               TrackGauge.Standard,
               null);

            _scale1 = factory.NewScale(
                new ScaleId(new Guid("fb7ab3fc-5f15-4e2c-a8d3-7ef2e615dae8")),
                "1",
                Slug.Of("1"),
                Ratio.Of(32.0M),
                Gauge.OfMillimiters(45.0M),
                TrackGauge.Standard,
                null);

            _scale0 = factory.NewScale(
                new ScaleId(new Guid("efc1fdb5-93aa-4a52-bbce-5ab67e92980c")),
                "0",
                Slug.Of("0"),
                Ratio.Of(43.5M),
                Gauge.OfMillimiters(32.0M),
                TrackGauge.Standard,
                null);

            _scaleN = factory.NewScale(
               new ScaleId(new Guid("f02ae69c-6a60-4fd4-bf5b-ac950e696361")),
               "N",
               Slug.Of("N"),
               Ratio.Of(160.0M),
               Gauge.OfMillimiters(9.0M),
               TrackGauge.Standard,
               null);

            _scaleZ = factory.NewScale(
               new ScaleId(new Guid("02790f5e-8edc-43f6-8ac1-4c906805d9ba")),
               "Z",
               Slug.Of("Z"),
               Ratio.Of(220.0M),
               Gauge.OfMillimiters(6.5M),
               TrackGauge.Standard,
               null);
            #endregion

            _all = new List<IScale>()
            {
                _scaleH0,
                _scaleH0m,
                _scaleH0e,
                _scale0,
                _scale1,
                _scaleTT,
                _scaleZ,
                _scaleN
            };
        }

        public ICollection<IScale> All() => _all;

        public IScale ScaleH0() => _scaleH0;

        public IScale ScaleH0m() => _scaleH0m;

        public IScale ScaleN() => _scaleN;

        public IScale Scale1() => _scale1;

        public IScale Scale0() => _scale0;

        public IScale ScaleZ() => _scaleZ;

        public IScale ScaleTT() => _scaleTT;

        public IScale ScaleH0e() => _scaleH0e;
    }

    public static class IScalesRepositoryExtensions
    {
        public static void SeedDatabase(this IScalesRepository repo)
        {
            var scales = CatalogSeedData.Scales.All();
            foreach (var scale in scales)
            {
                repo.Add(scale);
            }
        }
    }
}
