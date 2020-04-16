using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public class AddItemToCollectionOutputPort : OutputPortTestHelper<AddItemToCollectionOutput>, IAddItemToCollectionOutputPort
    {
        private MethodInvocation<string> CollectionNotFoundMethod { set; get; }
        private MethodInvocation<string> ShopNotFoundMethod { set; get; }

        public AddItemToCollectionOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(CollectionNotFound));
            ShopNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(ShopNotFoundMethod));
        }

        public void ShopNotFound(string message)
        {
            ShopNotFoundMethod = this.ShopNotFoundMethod.Invoked(message);
        }

        public void CollectionNotFound(string message)
        {
            CollectionNotFoundMethod = this.CollectionNotFoundMethod.Invoked(message);
        }

        public void ShouldHaveCollectionNotFoundMessage(string expectedMessage)
        {
            this.CollectionNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void ShouldHaveShopNotFoundMessage(string expectedMessage)
        {
            this.ShopNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }
    }
}
