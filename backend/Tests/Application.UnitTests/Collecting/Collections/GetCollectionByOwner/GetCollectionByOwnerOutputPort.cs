using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerOutputPort : OutputPortTestHelper<GetCollectionByOwnerOutput>, IGetCollectionByOwnerOutputPort
    {
        private MethodInvocation<Owner> CollectionNotFoundMethod { set; get; }

        public GetCollectionByOwnerOutputPort()
        {
            CollectionNotFoundMethod = MethodInvocation<Owner>.NotInvoked(nameof(CollectionNotFound));
        }

        public void CollectionNotFound(Owner owner)
        {
            CollectionNotFoundMethod = CollectionNotFoundMethod.Invoked(owner);
        }

        public void AssertCollectionWasNotFoundFor(Owner expectedOwner) =>
            CollectionNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedOwner);
    }
}