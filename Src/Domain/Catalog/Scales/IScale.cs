using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale : IScaleInfo
    {
        Gauge Gauge { get; }

        TrackGauge TrackGauge { get; }

        string? Notes { get; }

        DateTime? CreatedAt { get; }

        int? Version { get; }
    }
}
