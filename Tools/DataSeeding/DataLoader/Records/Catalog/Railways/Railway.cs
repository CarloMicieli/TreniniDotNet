using System;

namespace DataSeeding.DataLoader.Records.Catalog.Railways
{
    public sealed class Railway
    {
        public string Name { set; get; }
        public string RailwayLogo { set; get; }
        public string CompanyName { set; get; }
        public string Country { set; get; }
        public PeriodOfActivity PeriodOfActivity { set; get; }
        public RailwayGauge RailwayGauge { set; get; }
        public RailwayLength Length { set; get; }
        public string WebsiteUrl { set; get; }
        public string Headquarters { set; get; }
    }
}

