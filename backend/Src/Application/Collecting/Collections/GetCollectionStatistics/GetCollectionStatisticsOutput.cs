using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsOutput : IUseCaseOutput
    {
        public GetCollectionStatisticsOutput(CollectionStats statistics)
        {
            Statistics = statistics;
        }

        public CollectionStats Statistics { get; }
    }
}
