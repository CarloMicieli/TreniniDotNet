using System;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class RailwaysFactory : IRailwaysFactory
    {
        public IRailway NewRailway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            return new Railway
            {
                RailwayId = RailwayId.NewId(),
                Name = name,
                Slug = Slug.Of(name),
                CompanyName = companyName,
                Country = country,
                OperatingSince = operatingSince,
                OperatingUntil = operatingUntil,
                Status = rs
            };
        }
    }
}
