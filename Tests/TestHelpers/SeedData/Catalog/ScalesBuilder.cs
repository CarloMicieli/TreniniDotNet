using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Lengths;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class ScalesBuilder
    {
        private ScaleId _id;
        private string _name;
        private Slug _slug;
        private Ratio _ratio;
        private ScaleGauge _gauge;
        private string _description;
        private ISet<ScaleStandard> _standards = new HashSet<ScaleStandard>();
        private int? _weight;
        private readonly Instant _created;
        private readonly Instant? _modified;
        private readonly int _version;

        internal ScalesBuilder()
        {
            _id = ScaleId.NewId();
            _created = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _modified = null;
            _version = 1;
        }

        public ScalesBuilder Id(Guid id)
        {
            _id = new ScaleId(id);
            return this;
        }

        public ScalesBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public ScalesBuilder Slug(Slug slug)
        {
            _slug = slug;
            return this;
        }

        public ScalesBuilder Ratio(Ratio ratio)
        {
            _ratio = ratio;
            return this;
        }

        public ScalesBuilder Gauge(ScaleGauge gauge)
        {
            _gauge = gauge;
            return this;
        }

        public ScalesBuilder StandardGauge(Length length) =>
            Gauge(ScaleGauge.Of(length.Value, length.MeasureUnit));

        public ScalesBuilder NarrowGauge(Length length) =>
            Gauge(ScaleGauge.Of(length.Value, length.MeasureUnit, TrackGauge.Narrow));

        public ScalesBuilder Description(string description)
        {
            _description = description;
            return this;
        }

        public ScalesBuilder Standards(params ScaleStandard[] standards)
        {
            _standards = standards.ToHashSet();
            return this;
        }

        public ScalesBuilder Weight(int weight)
        {
            _weight = weight;
            return this;
        }

        public Scale Build() => new Scale(_id, _name, _slug, _ratio, _gauge, _description,
            _standards, _weight, _created, _modified, _version
        );
    }
}
