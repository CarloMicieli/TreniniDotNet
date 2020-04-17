using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerPresenter : DefaultHttpResultPresenter<GetCollectionByOwnerOutput>, IGetCollectionByOwnerOutputPort
    {
        public void CollectionNotFound(Owner owner)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(GetCollectionByOwnerOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
