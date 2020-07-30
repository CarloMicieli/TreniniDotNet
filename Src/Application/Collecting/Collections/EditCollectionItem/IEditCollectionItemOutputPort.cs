using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public interface IEditCollectionItemOutputPort : IStandardOutputPort<EditCollectionItemOutput>
    {
        void CollectionNotFound(Owner owner, CollectionId id);

        void CollectionItemNotFound(Owner owner, CollectionId id, CollectionItemId itemId);

        void ShopNotFound(string shop);
    }
}
