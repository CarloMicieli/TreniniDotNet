using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class EditCollectionItemOutputPort : OutputPortTestHelper<EditCollectionItemOutput>, IEditCollectionItemOutputPort
    {
        private MethodInvocation<string> CollectionNotFoundMethod { set; get; }
        private MethodInvocation<string> CollectionItemNotFoundMethod { set; get; }
        private MethodInvocation<string> ShopNotFoundMethod { set; get; }

        public EditCollectionItemOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(CollectionNotFound));
            CollectionItemNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(CollectionItemNotFound));
            ShopNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(ShopNotFound));
        }

        public void ShopNotFound(string message)
        {
            ShopNotFoundMethod = ShopNotFoundMethod.Invoked(message);
        }

        public void CollectionNotFound(string message)
        {
            CollectionNotFoundMethod = CollectionNotFoundMethod.Invoked(message);
        }

        public void CollectionItemNotFound(string message)
        {
            CollectionItemNotFoundMethod = CollectionItemNotFoundMethod.Invoked(message);
        }

        public void ShouldHaveCollectionNotFoundMessage(string expectedMessage)
        {
            CollectionNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void ShouldHaveCollectionItemNotFoundMessage(string expectedMessage)
        {
            CollectionItemNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void ShouldHaveShopNotFoundMessage(string expectedMessage)
        {
            ShopNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }
    }
}
