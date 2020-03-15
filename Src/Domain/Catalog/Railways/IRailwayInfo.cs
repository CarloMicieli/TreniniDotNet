using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwayInfo
    {
        RailwayId RailwayId { get; }

        Slug Slug { get; }

        string Name { get; }

        string? Country { get; }

        string ToLabel() => Name;

        IRailwayInfo ToRailwayInfo();
    }
}