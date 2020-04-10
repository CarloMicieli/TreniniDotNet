using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using NodaTime;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Uuid;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysFactory : IRailwaysFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public RailwaysFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IRailway NewRailway(
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
            DateTime? lastModified,
            int? version)
        {
            var railwayStatus = active == true ? RailwayStatus.Active : RailwayStatus.Inactive;
            var periodOfActivity = PeriodOfActivity.Of(railwayStatus.ToString(), operatingSince, operatingUntil);

            var country = Country.Of(countryCode!);

            var railwayLength = RailwayLength.TryCreate(totalLengthKm, totalLengthMi, out var rl) ? rl : null;

            var website = Uri.TryCreate(websiteUrl, UriKind.Absolute, out var uri) ? uri : null;

            var railwayGauge = RailwayGauge.TryCreate(trackGauge, gaugeIn, gaugeMm, out var rg) ? rg : null;

            Instant modified = lastModified.ToUtcOrGetCurrent(_clock);

            return new Railway(
                new RailwayId(railwayId),
                Slug.Of(slug),
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                railwayGauge,
                website,
                headquarters,
                modified,
                version ?? 1);
        }

        public IRailway NewRailway(RailwayId id,
            string name,
            Slug slug,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            return new Railway(
                id,
                slug,
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                gauge,
                websiteUrl,
                headquarters,
                _clock.GetCurrentInstant(),
                1);
        }
    }
}
