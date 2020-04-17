using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
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
