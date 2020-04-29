using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
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
