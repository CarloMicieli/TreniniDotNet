using System;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class Railway : AggregateRoot<RailwayId>
    {
        public Railway(
            RailwayId id,
            string name,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters,
            Instant created,
            Instant? modified,
            int version)
            : base(id, created, modified, version)
        {
            Name = name;
            Slug = Slug.Of(name);
            CompanyName = companyName;
            Country = country;
            PeriodOfActivity = periodOfActivity;
            TrackGauge = gauge;
            TotalLength = railwayLength;
            WebsiteUrl = websiteUrl;
            Headquarters = headquarters;
        }

        #region [ Properties ]
        public Slug Slug { get; }

        public string Name { get; }

        public string? CompanyName { get; }

        public Country Country { get; }

        public PeriodOfActivity PeriodOfActivity { get; }

        public RailwayGauge? TrackGauge { get; }

        public RailwayLength? TotalLength { get; }

        public Uri? WebsiteUrl { get; }

        public string? Headquarters { get; }
        #endregion

        public Railway With(
            string? name = null,
            string? companyName = null,
            Country? country = null,
            PeriodOfActivity? periodOfActivity = null,
            RailwayLength? railwayLength = null,
            RailwayGauge? gauge = null,
            Uri? websiteUrl = null,
            string? headquarters = null)
        {
            var slug = (name is null) ? Slug : Slug.Of(name);
            return new Railway(
                Id,
                name ?? Name,
                companyName ?? CompanyName,
                country ?? Country,
                periodOfActivity ?? PeriodOfActivity,
                railwayLength ?? TotalLength,
                gauge ?? TrackGauge,
                websiteUrl ?? WebsiteUrl,
                headquarters ?? Headquarters,
                CreatedDate,
                ModifiedDate,
                Version);
        }

        public override string ToString() => $"Railway({Name})";
    }
}
