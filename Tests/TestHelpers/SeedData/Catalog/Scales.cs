using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.TestHelpers.SeedData.Catalog.CatalogSeedData;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Scales
    {
        private static readonly IScalesFactory factory = new ScalesFactory(SystemClock.Instance, new GuidSource());

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
            _scaleH0 = NewScaleWith(
                new ScaleId(new Guid("7edfb586-218c-4997-8820-f61d3a81ce66")),
                "H0",
                ratio: Ratio.Of(87.0M),
                gauge: ScaleGauge.Of(16.5M, MeasureUnit.Millimeters));

            _scaleH0m = NewScaleWith(
               new ScaleId(new Guid("0dd13f9d-d730-41bb-b4e9-33218ea14fbc")),
               "H0m",
               ratio: Ratio.Of(87.0M),
               gauge: ScaleGauge.Of(12M, MeasureUnit.Millimeters, TrackGauge.Narrow));

            _scaleH0e = NewScaleWith(
                new ScaleId(new Guid("b5f2f033-a947-4b86-9d9e-52d7c1903ce0")),
                "H0e",
                ratio: Ratio.Of(87.0M),
                gauge: ScaleGauge.Of(9M, MeasureUnit.Millimeters, TrackGauge.Narrow));

            _scaleTT = NewScaleWith(
               new ScaleId(new Guid("374f5bb7-e7d1-4995-aa34-072b6b6500f9")),
               "TT",
               Ratio.Of(120.0M),
               gauge: ScaleGauge.Of(12M, MeasureUnit.Millimeters));

            _scale1 = NewScaleWith(
                new ScaleId(new Guid("fb7ab3fc-5f15-4e2c-a8d3-7ef2e615dae8")),
                "1",
                Ratio.Of(32.0M),
                gauge: ScaleGauge.Of(45M, MeasureUnit.Millimeters));

            _scale0 = NewScaleWith(
                new ScaleId(new Guid("efc1fdb5-93aa-4a52-bbce-5ab67e92980c")),
                "0",
                Ratio.Of(43.5M),
                gauge: ScaleGauge.Of(32M, MeasureUnit.Millimeters));

            _scaleN = NewScaleWith(
               new ScaleId(new Guid("f02ae69c-6a60-4fd4-bf5b-ac950e696361")),
               "N",
               Ratio.Of(160.0M),
               gauge: ScaleGauge.Of(9M, MeasureUnit.Millimeters));

            _scaleZ = NewScaleWith(
               new ScaleId(new Guid("02790f5e-8edc-43f6-8ac1-4c906805d9ba")),
               "Z",
               Ratio.Of(220.0M),
               gauge: ScaleGauge.Of(6.5M, MeasureUnit.Millimeters));
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

        public IList<IScale> All() => _all;

        public IScale ScaleH0() => _scaleH0;

        public IScale ScaleH0m() => _scaleH0m;

        public IScale ScaleN() => _scaleN;

        public IScale Scale1() => _scale1;

        public IScale Scale0() => _scale0;

        public IScale ScaleZ() => _scaleZ;

        public IScale ScaleTT() => _scaleTT;

        public IScale ScaleH0e() => _scaleH0e;
    }

    public static class ScalesRepositoryExtensions
    {
        public static void SeedDatabase(this IScalesRepository repo)
        {
            var scales = CatalogSeedData.Scales.All();
            foreach (var scale in scales)
            {
                repo.AddAsync(scale);
            }
        }
    }
}
