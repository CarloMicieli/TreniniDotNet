using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsOutput : IUseCaseOutput
    {
        public GetCollectionStatisticsOutput(ICollectionStats statistics)
        {
            Statistics = statistics;
        }

        public ICollectionStats Statistics { get; }

    }
}
