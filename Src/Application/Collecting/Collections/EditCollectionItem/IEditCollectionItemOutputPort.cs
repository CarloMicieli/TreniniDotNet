using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public interface IEditCollectionItemOutputPort : IOutputPortStandard<EditCollectionItemOutput>
    {
        void CollectionNotFound(Owner owner, CollectionId id);

        void CollectionItemNotFound(Owner owner, CollectionId id, CollectionItemId itemId);

        void ShopNotFound(string shop);
    }
}
