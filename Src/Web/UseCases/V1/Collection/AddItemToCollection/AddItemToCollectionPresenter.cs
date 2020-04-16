using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToCollection
{
    public sealed class AddItemToCollectionPresenter : DefaultHttpResultPresenter<AddItemToCollectionOutput>, IAddItemToCollectionOutputPort
    {
        public void CollectionNotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void ShopNotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(AddItemToCollectionOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
