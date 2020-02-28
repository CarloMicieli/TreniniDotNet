using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStock
    {
        IRailway Railway  { get; }

        IScale Scale  { get; }
        
        Category Category  { get; }

        Era Era { get; }

        //PowerMethod PowerMethod  { get; }

        Length Length { get; }
    }
}