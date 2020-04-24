using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics
{
    public interface IGetCollectionStatisticsOutputPort : IOutputPortStandard<GetCollectionStatisticsOutput>
    {
        void CollectionNotFound(Owner owner);
    }
}
