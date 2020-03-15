using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysFactory : IRailwaysFactory
    {
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
                rs);
            return railway;
        }

        public IRailway NewRailway(RailwayId id, string name, Slug slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs)
        {
            var railway = new Railway(
                id,
                Slug.Of(name),
                name,
                companyName,
                country,
                operatingSince,
                operatingUntil,
                rs);
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
                active == true ? RailwayStatus.Active : RailwayStatus.Inactive);
            return railway;
        }
    }
}
