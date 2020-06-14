using System;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.ValueObjects;

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
            DateTime created,
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
                created.ToUtc(),
                modified,
                version ?? 1);
        }

        public IRailway CreateNewRailway(
            string name,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            return new Railway(
                new RailwayId(_guidSource.NewGuid()),
                Slug.Of(name),
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                gauge,
                websiteUrl,
                headquarters,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public IRailway UpdateRailway(
            IRailway railway,
            string? name,
            string? companyName,
            Country? country,
            PeriodOfActivity? periodOfActivity,
            RailwayLength? totalLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            Slug slug = (name is null) ? railway.Slug : Slug.Of(name);
            return new Railway(
                railway.Id,
                slug,
                name ?? railway.Name,
                companyName ?? railway.CompanyName,
                country ?? railway.Country,
                periodOfActivity ?? railway.PeriodOfActivity,
                totalLength ?? railway.TotalLength,
                gauge ?? railway.TrackGauge,
                websiteUrl ?? railway.WebsiteUrl,
                headquarters ?? railway.Headquarters,
                railway.CreatedDate,
                _clock.GetCurrentInstant(),
                railway.Version + 1);
        }
    }
}
