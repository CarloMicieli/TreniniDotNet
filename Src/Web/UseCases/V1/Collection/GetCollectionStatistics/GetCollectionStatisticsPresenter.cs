using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsPresenter : DefaultHttpResultPresenter<GetCollectionStatisticsOutput>, IGetCollectionStatisticsOutputPort
    {
        public void CollectionNotFound(Owner owner)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(GetCollectionStatisticsOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
