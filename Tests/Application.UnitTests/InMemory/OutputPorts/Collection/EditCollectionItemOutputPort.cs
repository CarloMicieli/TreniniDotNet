using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class EditCollectionItemOutputPort : OutputPortTestHelper<EditCollectionItemOutput>, IEditCollectionItemOutputPort
    {
        private MethodInvocation<Owner, CollectionId> CollectionNotFoundMethod { set; get; }
        private MethodInvocation<Owner, CollectionId, CollectionItemId> CollectionItemNotFoundMethod { set; get; }
        private MethodInvocation<string> ShopNotFoundMethod { set; get; }

        public EditCollectionItemOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<Owner, CollectionId>.NotInvoked(nameof(CollectionNotFound));
            CollectionItemNotFoundMethod = MethodInvocation<Owner, CollectionId, CollectionItemId>.NotInvoked(nameof(CollectionItemNotFound));
            ShopNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(ShopNotFound));
        }

        public void ShopNotFound(string shop)
        {
            ShopNotFoundMethod = ShopNotFoundMethod.Invoked(shop);
        }

        public void CollectionNotFound(Owner owner, CollectionId id)
        {
            CollectionNotFoundMethod = CollectionNotFoundMethod.Invoked(owner, id);
        }

        public void CollectionItemNotFound(Owner owner, CollectionId id, CollectionItemId itemId)
        {
            CollectionItemNotFoundMethod = CollectionItemNotFoundMethod.Invoked(owner, id, itemId);
        }

        public void ShouldHaveCollectionNotFoundMessage(Owner expectedOwner, CollectionId expectedId)
        {
            CollectionNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedOwner, expectedId);
        }

        public void ShouldHaveCollectionItemNotFoundMessage(Owner expectedOwner, CollectionId expectedId, CollectionItemId expectedItemId)
        {
            CollectionItemNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedOwner, expectedId, expectedItemId);
        }

        public void ShouldHaveShopNotFoundMessage(string expectedShop)
        {
            ShopNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedShop);
        }
    }
}
