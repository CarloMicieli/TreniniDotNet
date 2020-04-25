using NodaTime;
using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public class FakeRailway : IRailway
    {
        public string CompanyName { set; get; }

        public PeriodOfActivity PeriodOfActivity { set; get; }

        public RailwayGauge TrackGauge { set; get; }

        public RailwayLength TotalLength { set; get; }

        public Uri WebsiteUrl { set; get; }

        public string Headquarters { set; get; }

        public Instant CreatedDate { set; get; }

        public Instant? ModifiedDate { set; get; }

        public int Version { set; get; }

        public RailwayId RailwayId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; }

        public Country Country { set; get; }

        public FakeRailway()
        {
            RailwayId = new RailwayId(new Guid("b9e62d18-06e3-4404-9183-6c3a3b89c683"));
            Slug = Slug.Of("FS");
            Name = "FS";
            CompanyName = "Ferrovie dello Stato";
            Version = 1;
            Country = Country.Of("IT");
            PeriodOfActivity = PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1));
            TrackGauge = null;
            TotalLength = null;
            WebsiteUrl = new Uri("https://www.trenitalia.com");
            Headquarters = null;
            CreatedDate = Instant.FromUtc(1988, 11, 25, 9, 0);
            ModifiedDate = null;
        }

        public IRailway With(string companyName = null)
        {
            CompanyName = companyName;
            return this;
        }

        public IRailwayInfo ToRailwayInfo() => this;
    }
}
