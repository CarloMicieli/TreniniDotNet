using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts
{
    public sealed class GetCollectionStatisticsOutputPort : OutputPortTestHelper<GetCollectionStatisticsOutput>, IGetCollectionStatisticsOutputPort
    {
        private MethodInvocation<Owner> CollectionNotFoundMethod { set; get; }

        public GetCollectionStatisticsOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<Owner>.NotInvoked(nameof(CollectionNotFound));
        }

        public void CollectionNotFound(Owner owner)
        {
            CollectionNotFoundMethod = CollectionNotFoundMethod.Invoked(owner);
        }

        public void AssertCollectionWasNotFoundForOwner(Owner expectedOwner)
        {
            CollectionNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedOwner);
        }
    }
}
