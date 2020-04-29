using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public interface IAddItemToCollectionOutputPort : IOutputPortStandard<AddItemToCollectionOutput>
    {
        void CollectionNotFound(Owner owner);

        void ShopNotFound(string shopName);

        void CatalogItemNotFound(Slug catalogItem);
    }
}
