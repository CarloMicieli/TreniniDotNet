using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
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