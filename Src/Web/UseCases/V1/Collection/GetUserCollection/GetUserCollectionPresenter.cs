using TreniniDotNet.Application.Boundaries.Collection.GetUserCollection;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetUserCollection
{
    public sealed class GetUserCollectionPresenter : DefaultHttpResultPresenter<GetUserCollectionOutput>, IGetUserCollectionOutputPort
    {
        public override void Standard(GetUserCollectionOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
