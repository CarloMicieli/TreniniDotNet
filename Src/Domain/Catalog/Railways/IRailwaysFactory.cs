using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysFactory
    {
        IRailway NewRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs);

        IRailway NewRailway(RailwayId id, string name, Slug slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs);
        
        IRailway? NewRailway(Guid railway_id, string name, string slug, string? company_name, string? country, DateTime? operating_since, DateTime? operating_until, bool? active, DateTime? created_at, int? version);
    }
}
