using System;
using System.Runtime.CompilerServices;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class Railway : AggregateRoot<RailwayId>, IRailway
    {
        internal Railway(
            RailwayId id,
            Slug slug, string name,
            string? companyName,
            Country? country,
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
            Slug = slug;
            Name = name;
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

        public Country? Country { get; }

        public PeriodOfActivity PeriodOfActivity { get; }

        public RailwayGauge? TrackGauge { get; }

        public RailwayLength? TotalLength { get; }

        public Uri? WebsiteUrl { get; }

        public string? Headquarters { get; }
        #endregion

        public override string ToString() => $"Railway({Name})";

        public IRailwayInfo ToRailwayInfo() => this;
    }
}
