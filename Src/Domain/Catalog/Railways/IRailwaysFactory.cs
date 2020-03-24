using System;
using LanguageExt;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysFactory
    {
        Validation<Error, IRailway> NewRailwayV(Guid railwayId,
            string name,
            string? companyName,
            string? country,
            DateTime? operatingSince, DateTime? operatingUntil,
            bool? active);

        IRailway NewRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs);

        IRailway NewRailway(RailwayId id, string name, Slug slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs);

        IRailway? NewRailway(Guid railwayId, string name, string slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, bool? active, DateTime? createdAt, int? version);
    }
}
