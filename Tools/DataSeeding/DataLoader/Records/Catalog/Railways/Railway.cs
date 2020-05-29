using System;

namespace DataSeeding.DataLoader.Records.Catalog.Railways
{
    public sealed class Railway
    {
        public Guid RailwayId { set; get; }
        public string Name { set; get; }
        public string Slug { set; get; }
        public string RailwayLogo { set; get; }
        public string CompanyName { set; get; }
        public string Country { set; get; }
        public PeriodOfActivity PeriodOfActivity { set; get; }
        public int? TrackGauge { set; get; }
        public int? Length { set; get; }
        public string Website { set; get; }
        public int Version { set; get; }
        public DateTime LastModified { set; get; }
    }
}