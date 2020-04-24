using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Collection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerPresenter : DefaultHttpResultPresenter<GetCollectionByOwnerOutput>, IGetCollectionByOwnerOutputPort
    {
        public void CollectionNotFound(Owner owner)
        {
            ViewModel = new NotFoundObjectResult(new { Owner = owner.Value });
        }

        public override void Standard(GetCollectionByOwnerOutput output)
        {
            var viewModel = new CollectionView(output.Collection);
            ViewModel = new OkObjectResult(viewModel);
        }
    }
}
