using System;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysFactory : IRailwaysFactory
    {
        public IRailway NewRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            throw new NotImplementedException();
        }
    }
}
