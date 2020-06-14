using System;
using NodaTime;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailway : IRailwayInfo
    {
        string? CompanyName { get; }

        Country? Country { get; }

        PeriodOfActivity PeriodOfActivity { get; }

        RailwayGauge? TrackGauge { get; }

        RailwayLength? TotalLength { get; }

        Uri? WebsiteUrl { get; }

        string? Headquarters { get; }

        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }

        IRailwayInfo ToRailwayInfo();

        IRailway With(
            string? companyName = null,
            Country? country = null,
            PeriodOfActivity? periodOfActivity = null,
            RailwayLength? railwayLength = null,
            RailwayGauge? gauge = null,
            Uri? websiteUrl = null,
            string? headquarters = null) => new Railway(
                Id,
                Slug,
                Name,
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
}
