using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwayInfo
    {
        Slug Slug { get; }

        string Name { get; }

        string? Country { get; }

        string ToLabel() => Name;

        IRailwayInfo ToRailwayInfo();
    }
}