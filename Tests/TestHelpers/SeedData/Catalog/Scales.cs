using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Lengths;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Scales
    {
        private readonly Scale _scaleH0;
        private readonly Scale _scaleH0m;
        private readonly Scale _scaleH0e;
        private readonly Scale _scaleTT;
        private readonly Scale _scale1;
        private readonly Scale _scale0;
        private readonly Scale _scaleN;
        private readonly Scale _scaleZ;

        private readonly IList<Scale> _all;

        public ScalesBuilder New() => new ScalesBuilder();

        public Scales()
        {
            #region [ Init data ]

            _scaleH0 = New()
                .Id(new Guid("7edfb586-218c-4997-8820-f61d3a81ce66"))
                .Name("H0")
                .Slug(Slug.Of("H0"))
                .Ratio(Ratio.Of(87.0M))
                .StandardGauge(Length.OfMillimeters(16.5M))
                .Build();

            _scaleH0m = New()
                .Id(new Guid("0dd13f9d-d730-41bb-b4e9-33218ea14fbc"))
                .Name("H0m")
                .Slug(Slug.Of("H0m"))
                .Ratio(Ratio.Of(87.0M))
                .NarrowGauge(Length.OfMillimeters(12.0M))
                .Build();

            _scaleH0e = New()
                .Id(new Guid("b5f2f033-a947-4b86-9d9e-52d7c1903ce0"))
                .Name("H0e")
                .Slug(Slug.Of("H0e"))
                .Ratio(Ratio.Of(87.0M))
                .NarrowGauge(Length.OfMillimeters(9.0M))
                .Build();

            _scaleTT = New()
                .Id(new Guid("374f5bb7-e7d1-4995-aa34-072b6b6500f9"))
                .Name("TT")
                .Slug(Slug.Of("TT"))
                .Ratio(Ratio.Of(120.0M))
                .StandardGauge(Length.OfMillimeters(12.0M))
                .Build();

            _scale1 = New()
                .Id(new Guid("fb7ab3fc-5f15-4e2c-a8d3-7ef2e615dae8"))
                .Name("1")
                .Slug(Slug.Of("1"))
                .Ratio(Ratio.Of(32.0M))
                .StandardGauge(Length.OfMillimeters(45.0M))
                .Build();

            _scale0 = New()
                .Id(new Guid("efc1fdb5-93aa-4a52-bbce-5ab67e92980c"))
                .Name("0")
                .Slug(Slug.Of("0"))
                .Ratio(Ratio.Of(43.5M))
                .StandardGauge(Length.OfMillimeters(32.0M))
                .Build();

            _scaleN = New()
                .Id(new Guid("f02ae69c-6a60-4fd4-bf5b-ac950e696361"))
                .Name("N")
                .Slug(Slug.Of("N"))
                .Ratio(Ratio.Of(160.0M))
                .StandardGauge(Length.OfMillimeters(9.0M))
                .Build();

            _scaleZ = New()
                .Id(new Guid("02790f5e-8edc-43f6-8ac1-4c906805d9ba"))
                .Name("Z")
                .Slug(Slug.Of("Z"))
                .Ratio(Ratio.Of(220.0M))
                .StandardGauge(Length.OfMillimeters(6.5M))
                .Build();

            #endregion

            _all = new List<Scale>()
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

        public IList<Scale> All() => _all;

        public Scale ScaleH0() => _scaleH0;

        public Scale ScaleH0m() => _scaleH0m;

        public Scale ScaleN() => _scaleN;

        public Scale Scale1() => _scale1;

        public Scale Scale0() => _scale0;

        public Scale ScaleZ() => _scaleZ;

        public Scale ScaleTT() => _scaleTT;

        public Scale ScaleH0e() => _scaleH0e;

    }

    public static class ScalesRepositoryExtensions
    {
        public static async Task SeedDatabase(this IScalesRepository repo)
        {
            var scales = CatalogSeedData.Scales.All();
            foreach (var scale in scales)
            {
                await repo.AddAsync(scale);
            }
        }
    }
}
