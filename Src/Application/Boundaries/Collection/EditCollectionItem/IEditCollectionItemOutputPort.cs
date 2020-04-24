using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem
{
    public interface IEditCollectionItemOutputPort : IOutputPortStandard<EditCollectionItemOutput>
    {
        void CollectionNotFound(Owner owner, CollectionId id);

        void CollectionItemNotFound(Owner owner, CollectionId id, CollectionItemId itemId);

        void ShopNotFound(string shop);
    }
}
