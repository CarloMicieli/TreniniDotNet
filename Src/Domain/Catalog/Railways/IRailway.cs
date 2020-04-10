using NodaTime;
using System;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailway : IRailwayInfo
    {
        string? CompanyName { get; }

        PeriodOfActivity PeriodOfActivity { get; }

        RailwayGauge? TrackGauge { get; }

        RailwayLength? TotalLength { get; }

        Uri? WebsiteUrl { get; }

        string? Headquarters { get; }

        int? Version { get; }

        Instant? LastModifiedAt { get; }

        IRailwayInfo ToRailwayInfo();
    }
}
