using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public sealed class Scale : IScale
    {
        public ScaleId ScaleId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; } = null!;

        public Ratio Ratio { set; get; }

        public Gauge Gauge { set; get; }

        public TrackGauge TrackGauge { set; get; }

        public string? Notes { set; get; }
    }
}
