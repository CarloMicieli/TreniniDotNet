using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Globalization;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysFactory : IRailwaysFactory
    {
        public Validation<Error, IRailway> NewRailwayV(
            Guid railwayId,
            string railwayName,
            string? companyName,
            string? country,
            DateTime? operatingSince, DateTime? operatingUntil,
            bool? active)
        {
            var railwayNameV = ToRailwayName(railwayName);
            var railwayStatusV = ToRailwayStatus(active ?? true);
            var countryV = ToRegionInfo(country);
            var periodOfActivityV = PeriodOfActivity.TryCreate(operatingSince, operatingUntil, active);

            return (railwayNameV, countryV, railwayStatusV, periodOfActivityV).Apply((name, ri, rs, poa) =>
            {
                IRailway r = new Railway(
                    new RailwayId(railwayId),
                    Slug.Of(name),
                    name,
                    companyName,
                    ri,
                    poa,
                    DateTime.UtcNow,
                    1
                );
                return r;
            });
        }

        public IRailway NewRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            var id = RailwayId.NewId();
            var railway = new Railway(
                id,
                Slug.Of(name),
                name,
                companyName,
                country,
                operatingSince,
                operatingUntil,
                rs,
                DateTime.UtcNow,
                1);
            return railway;
        }

        public IRailway NewRailway(RailwayId id, string name, Slug slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs)
        {
            var railway = new Railway(
                id,
                slug,
                name,
                companyName,
                country,
                operatingSince,
                operatingUntil,
                rs,
                DateTime.UtcNow,
                1);
            return railway;
        }

        public IRailway? NewRailway(Guid railwayId, string name, string slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, bool? active, DateTime? createdAt, int? version)
        {
            var railway = new Railway(
                new RailwayId(railwayId),
                Slug.Of(slug),
                name,
                companyName,
                country,
                operatingSince,
                operatingUntil,
                active == true ? RailwayStatus.Active : RailwayStatus.Inactive,
                createdAt,
                version ?? 1);
            return railway;
        }

        public static Validation<Error, string> ToRailwayName(string? str) =>
            string.IsNullOrWhiteSpace(str) == false ? Success<Error, string>(str!) : Fail<Error, string>(Error.New("invalid railway: name cannot be empty"));

        public static Validation<Error, RegionInfo?> ToRegionInfo(string? country)
        {
            if (country is null)
            {
                return Success<Error, RegionInfo?>(null);
            }

            try
            {
                var regionInfo = new RegionInfo(country?.ToUpper());
                return Success<Error, RegionInfo?>(regionInfo);
            }
            catch
            {
                return Fail<Error, RegionInfo?>(Error.New($"'{country}' is not a valid country code."));
            }
        }

        public static Validation<Error, RailwayStatus> ToRailwayStatus(bool active) =>
            Success<Error, RailwayStatus>(active ? RailwayStatus.Active : RailwayStatus.Inactive);

    }
}
