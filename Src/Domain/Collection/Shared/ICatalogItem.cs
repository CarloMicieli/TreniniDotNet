using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public interface ICatalogItem
    {
        Slug Slug { get; }

        string Brand { get; }

        ItemNumber ItemNumber { get; }
    }
}
