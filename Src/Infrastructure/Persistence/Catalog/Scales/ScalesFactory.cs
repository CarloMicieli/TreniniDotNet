using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    [Obsolete()]
    public sealed class ScalesFactory2 : IScalesFactory
    {
        public IScale NewScale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            return new Scale
            {
                ScaleId = ScaleId.NewId(),
                Name = name,
                Slug = Slug.Of(name),
                Ratio = ratio,
                Gauge = gauge,
                TrackGauge = trackGauge,
                Notes = notes
            };
        }

        public IScale NewScale(string name, decimal? ratio, decimal? gauge, TrackGauge trackGauge)
        {
            throw new System.NotImplementedException();
        }
    }
}
