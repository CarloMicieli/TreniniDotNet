using NodaTime;
using System;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public class FakeScale : IScale
    {
        public ScaleGauge Gauge { set; get; }

        public string Description { set; get; }

        public int? Weight { set; get; }

        public IImmutableSet<ScaleStandard> Standards { set; get; }

        public Instant CreatedDate { set; get; }

        public Instant? ModifiedDate { set; get; }

        public int Version { set; get; }

        public ScaleId ScaleId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; }

        public Ratio Ratio { set; get; }

        public FakeScale()
            : this(new Guid("66c9e10f-0382-4e4b-8986-1f41bc883347"), "H0")
        {
        }

        public FakeScale(Guid id, string name)
        {
            ScaleId = new ScaleId(id);
            Slug = Slug.Of(name);
            Name = name;
            Gauge = ScaleGauge.Of(16.5M, 0.65M, TrackGauge.Standard.ToString());
            Description = null;
            Ratio = Ratio.Of(87M);
            Weight = null;
            Standards = ImmutableHashSet<ScaleStandard>.Empty;
            CreatedDate = Instant.FromUtc(1988, 11, 25, 9, 0);
            ModifiedDate = null;
            Version = 1;
        }

        public IScale With(decimal? ratio)
        {
            if (ratio.HasValue)
            {
                Ratio = Ratio.Of(ratio.Value);
            }

            return this;
        }

        public IScaleInfo ToScaleInfo() => this;
    }
}
