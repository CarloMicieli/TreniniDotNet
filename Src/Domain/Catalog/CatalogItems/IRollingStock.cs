using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStock
    {
        IRailway Railway { get; }

        Category Category { get; }

        Era Era { get; }

        Length Length { get; }

        string? ClassName { get; }

        string? RoadNumber { get; }
    }
}