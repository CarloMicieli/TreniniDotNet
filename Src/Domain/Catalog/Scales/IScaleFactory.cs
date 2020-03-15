using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesFactory
    {
        IScale NewScale(Guid id, string name, string slug, decimal? ratio, decimal? gauge, string? trackGauge, string? notes);

        IScale NewScale(ScaleId id, string name, Slug slug, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes);
    }
}
