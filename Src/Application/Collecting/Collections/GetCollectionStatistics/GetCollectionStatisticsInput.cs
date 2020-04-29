using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
