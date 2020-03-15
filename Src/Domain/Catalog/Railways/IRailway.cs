using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailway : IRailwayInfo
    {
        string? CompanyName { get; }

        RailwayStatus? Status { get; }

        DateTime? OperatingUntil { get; }

        DateTime? OperatingSince { get; }

        int? Version { get; }

        DateTime? CreatedAt { get; }
    }
}
