using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionPresenter : DefaultHttpResultPresenter<RemoveItemFromCollectionOutput>, IRemoveItemFromCollectionOutputPort
    {
        public override void Standard(RemoveItemFromCollectionOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
