using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStock
    {
        RollingStockId RollingStockId { get; }

        IRailwayInfo Railway { get; }

        Category Category { get; }

        Epoch Epoch { get; }

        LengthOverBuffer? Length { get; }

        string? ClassName { get; }

        string? RoadNumber { get; }

        string? TypeName { get; }

        PassengerCarType? PassengerCarType { get; }

        ServiceLevel? ServiceLevel { get; }

        DccInterface DccInterface { get; }

        Control Control { get; }
    }
}
