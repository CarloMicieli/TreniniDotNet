using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
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
