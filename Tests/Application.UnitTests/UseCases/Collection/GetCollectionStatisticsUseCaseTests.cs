using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class GetCollectionStatisticsUseCaseTests : UseCaseTestHelper<GetCollectionStatistics, GetCollectionStatisticsOutput, GetCollectionStatisticsOutputPort>
    {
    }
}
