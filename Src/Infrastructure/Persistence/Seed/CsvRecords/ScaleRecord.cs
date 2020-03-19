using System;

namespace TreniniDotNet.Infrastructure.Persistence.Seed.CsvRecords
{
    public sealed class ScaleRecord
    {
        public Guid ScaleId { set; get; }
        public string Name { set; get; } = null!;
        public string Slug { set; get; } = null!;
        public decimal Gauge { set; get; }
        public decimal Ratio { set; get; }
        public string? TrackGauge { set; get; }
    }
}
