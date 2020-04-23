using System;
using System.Collections.Generic;

namespace DataSeeding.Records.Catalog
{
    public sealed class Scales : DataSet<Scale> { }

    public sealed class Scale
    {
        public Guid ScaleId { set; get; }
        public string Slug { set; get; }
        public string Name { set; get; }
        public List<string> Standards { set; get; }
        public int Weight { set; get; }
        public List<Gauge> Gauge { set; get; }
        public decimal Ratio { set; get; }
        public string TrackGauge { set; get; }
        public int Version { set; get; }
        public DateTime LastModified { set; get; }
    }

    public sealed class Gauge
    {
        public decimal Value { set; get; }
        public string MeasureUnit { set; get; }
    }
}
