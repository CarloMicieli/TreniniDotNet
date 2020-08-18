using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public static class NewGetCollectionStatisticsInput
    {
        public static GetCollectionStatisticsInput With(string owner) =>
            new GetCollectionStatisticsInput(owner);
    }
}
