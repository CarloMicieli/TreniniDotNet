using System;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysFactory
    {
        IRailway NewRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs);
    }
}
