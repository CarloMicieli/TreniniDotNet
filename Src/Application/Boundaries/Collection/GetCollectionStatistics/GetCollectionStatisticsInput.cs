using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics
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
