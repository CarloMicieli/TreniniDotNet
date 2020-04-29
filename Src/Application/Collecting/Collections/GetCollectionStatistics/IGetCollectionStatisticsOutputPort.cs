using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public interface IGetCollectionStatisticsOutputPort : IOutputPortStandard<GetCollectionStatisticsOutput>
    {
        void CollectionNotFound(Owner owner);
    }
}
