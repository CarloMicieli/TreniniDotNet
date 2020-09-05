using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public interface IAddItemToCollectionOutputPort : IStandardOutputPort<AddItemToCollectionOutput>
    {
        void CollectionNotFound(Owner owner);

        void ShopNotFound(string shopName);

        void CatalogItemNotFound(Slug catalogItem);
    }
}
