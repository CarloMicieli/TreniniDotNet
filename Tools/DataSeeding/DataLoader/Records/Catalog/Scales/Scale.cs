using System.Collections.Generic;

namespace DataSeeding.DataLoader.Records.Catalog.Scales
{
    public sealed class Scale
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public List<string> Standards { set; get; }
        public int? Weight { set; get; }
        public ScaleGauge Gauge { set; get; }
        public float Ratio { set; get; }
    }
}
