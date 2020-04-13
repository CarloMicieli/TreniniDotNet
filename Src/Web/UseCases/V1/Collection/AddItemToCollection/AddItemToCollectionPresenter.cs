using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToCollection
{
    public sealed class AddItemToCollectionPresenter : DefaultHttpResultPresenter<AddItemToCollectionOutput>, IAddItemToCollectionOutputPort
    {
        public override void Standard(AddItemToCollectionOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
