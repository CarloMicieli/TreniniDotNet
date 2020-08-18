using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public interface IGetCollectionStatisticsOutputPort : IStandardOutputPort<GetCollectionStatisticsOutput>
    {
        void CollectionNotFound(Owner owner);
    }
}
