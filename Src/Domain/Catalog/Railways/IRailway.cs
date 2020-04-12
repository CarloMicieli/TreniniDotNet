using System;
using TreniniDotNet.Common.Entities;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailway : IModifiableEntity, IRailwayInfo
    {
        string? CompanyName { get; }

        PeriodOfActivity PeriodOfActivity { get; }

        RailwayGauge? TrackGauge { get; }

        RailwayLength? TotalLength { get; }

        Uri? WebsiteUrl { get; }

        string? Headquarters { get; }

        IRailwayInfo ToRailwayInfo();
    }
}
