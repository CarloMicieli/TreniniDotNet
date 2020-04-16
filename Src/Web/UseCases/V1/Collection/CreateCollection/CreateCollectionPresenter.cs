using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection
{
    public sealed class CreateCollectionPresenter : DefaultHttpResultPresenter<CreateCollectionOutput>, ICreateCollectionOutputPort
    {
        public override void Standard(CreateCollectionOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void UserHasAlreadyOneCollection(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
