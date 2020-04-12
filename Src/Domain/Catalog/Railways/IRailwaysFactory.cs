using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysFactory
    {
        IRailway NewRailway(RailwayId id,
            string name,
            Slug slug,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters);

        IRailway NewRailway(
               Guid railwayId,
               string name,
               string slug,
               string? companyName,
               string? countryCode,
               DateTime? operatingSince,
               DateTime? operatingUntil,
               bool? active,
               decimal? gaugeMm,
               decimal? gaugeIn,
               string? trackGauge,
               string? headquarters,
               decimal? totalLengthMi,
               decimal? totalLengthKm,
               string? websiteUrl,
               DateTime created,
               DateTime? lastModified,
               int? version);
    }
}
