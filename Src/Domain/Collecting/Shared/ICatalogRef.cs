using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public interface ICatalogRef
    {
        CatalogItemId CatalogItemId { get; }

        Slug Slug { get; }
    }
}
