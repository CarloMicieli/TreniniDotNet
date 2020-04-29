using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts
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
