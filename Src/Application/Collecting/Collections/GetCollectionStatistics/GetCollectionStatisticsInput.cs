using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsInput : IUseCaseInput
    {
        public GetCollectionStatisticsInput(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; }
    }
}
