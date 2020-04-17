using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection
{
    public interface IAddItemToCollectionOutputPort : IOutputPortStandard<AddItemToCollectionOutput>
    {
        void CollectionNotFound(Owner owner);

        void ShopNotFound(string shopName);

        void CatalogItemNotFound(Slug catalogItem);
    }
}
