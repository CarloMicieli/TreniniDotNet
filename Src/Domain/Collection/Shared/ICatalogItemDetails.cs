using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public interface ICatalogItemDetails
    {
        IBrandRef Brand { get; }
        ItemNumber ItemNumber { get; }
        CollectionCategory Category { get; }
        IScaleRef Scale { get; }
        int RollingStocksCount { get; }
        string Description { get; }
    }
}
