using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public sealed class Scale
    {
        public Guid ScaleId { set; get; }
        public string Slug { set; get; } = null!;
        public string Name { set; get; } = null!;
        public decimal Ratio { set; get; }
        public decimal Gauge { set; get; }
        public string? TrackGauge { set; get; }
        public string? Notes { set; get; }
        public int Version { set; get; } = 1;
        public DateTime CreationTime { set; get; }
    }
}
