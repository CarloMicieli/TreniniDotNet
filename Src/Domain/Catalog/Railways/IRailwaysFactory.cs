using System;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysFactory
    {
        IRailway CreateNewRailway(
            string name,
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

        IRailway UpdateRailway(
            IRailway railway,
            string? name,
            string? companyName,
            Country? country,
            PeriodOfActivity? periodOfActivity,
            RailwayLength? totalLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters);
    }
}
