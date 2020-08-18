using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionStatistics
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
