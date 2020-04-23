using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;
using TreniniDotNet.Web.ViewModels.V1.Collection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsPresenter : DefaultHttpResultPresenter<GetCollectionStatisticsOutput>, IGetCollectionStatisticsOutputPort
    {
        public void CollectionNotFound(Owner owner)
        {
            ViewModel = new NotFoundObjectResult(new { Owner = owner.Value });
        }

        public override void Standard(GetCollectionStatisticsOutput output)
        {
            ViewModel = new OkObjectResult(new CollectionStatisticsView(output.Statistics));
        }
    }
}
