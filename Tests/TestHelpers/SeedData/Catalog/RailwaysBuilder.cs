using System;
using NodaTime;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class RailwaysBuilder
    {
        private RailwayId _id;
        private string _name;
        private string _companyName;
        private Country _country;
        private PeriodOfActivity _periodOfActivity;
        private RailwayLength _railwayLength;
        private RailwayGauge _gauge;
        private Uri _websiteUrl;
        private string _headquarters;
        private readonly Instant _created;
        private readonly Instant? _modified;
        private readonly int _version;

        internal RailwaysBuilder()
        {
            _id = RailwayId.NewId();
            _created = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _modified = null;
            _version = 1;
        }

        public RailwaysBuilder Id(Guid id)
        {
            _id = new RailwayId(id);
            return this;
        }

        public RailwaysBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public RailwaysBuilder CompanyName(string companyName)
        {
            _companyName = companyName;
            return this;
        }

        public RailwaysBuilder Country(Country country)
        {
            _country = country;
            return this;
        }

        public RailwaysBuilder PeriodOfActivity(PeriodOfActivity periodOfActivity)
        {
            _periodOfActivity = periodOfActivity;
            return this;
        }

        public RailwaysBuilder RailwayLength(RailwayLength railwayLength)
        {
            _railwayLength = railwayLength;
            return this;
        }

        public RailwaysBuilder RailwayGauge(RailwayGauge gauge)
        {
            _gauge = gauge;
            return this;
        }

        public RailwaysBuilder WebsiteUrl(Uri websiteUrl)
        {
            _websiteUrl = websiteUrl;
            return this;
        }

        public RailwaysBuilder Headquarters(string headquarters)
        {
            _headquarters = headquarters;
            return this;
        }

        public Railway Build() => new Railway(
            _id,
            _name,
            _companyName,
            _country,
            _periodOfActivity,
            _railwayLength,
            _gauge,
            _websiteUrl,
            _headquarters,
            _created,
            _modified,
            _version);
    }
}
