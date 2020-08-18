using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionByOwner
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
