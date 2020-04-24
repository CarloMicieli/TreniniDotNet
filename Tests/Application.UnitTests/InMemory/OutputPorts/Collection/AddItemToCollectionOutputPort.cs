using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public class AddItemToCollectionOutputPort : OutputPortTestHelper<AddItemToCollectionOutput>, IAddItemToCollectionOutputPort
    {
        private MethodInvocation<Owner> CollectionNotFoundMethod { set; get; }
        private MethodInvocation<string> ShopNotFoundMethod { set; get; }
        private MethodInvocation<Slug> CatalogItemNotFoundMethod { set; get; }

        public AddItemToCollectionOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<Owner>.NotInvoked(nameof(CollectionNotFound));
            ShopNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(ShopNotFoundMethod));
            CatalogItemNotFoundMethod = MethodInvocation<Slug>.NotInvoked(nameof(CatalogItemNotFound));
        }

        public void ShopNotFound(string message)
        {
            ShopNotFoundMethod = this.ShopNotFoundMethod.Invoked(message);
        }

        public void CollectionNotFound(Owner owner)
        {
            CollectionNotFoundMethod = this.CollectionNotFoundMethod.Invoked(owner);
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            CatalogItemNotFoundMethod = this.CatalogItemNotFoundMethod.Invoked(catalogItem);
        }

        public void AssertCollectionWasNotFoundForOwner(Owner owner)
        {
            this.CollectionNotFoundMethod.ShouldBeInvokedWithTheArgument(owner);
        }

        public void ShouldHaveShopNotFoundMessage(string expectedMessage)
        {
            this.ShopNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void AssertCatalogItemNotFoundForSlug(Slug catalogItem)
        {
            this.CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument(catalogItem);
        }
    }
}
