using System;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Responses
{
    internal class RailwayResponse
    {
        public Guid Id { set; get; }
        public SelfLinks _Links { set; get; }
        public string Slug { set; get; }
        public string Name { set; get; }
        public string CompanyName { set; get; }
        public string Country { set; get; }
        public PeriodOfActivityResponse PeriodOfActivity { set; get; }
        public string WebsiteUrl { set; get; }
        public decimal? TrackGauge { set; get; }
        public decimal? TotalLength { set; get; }
    }
}