using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public sealed class CreateCollectionOutputPort : OutputPortTestHelper<CreateCollectionOutput>, ICreateCollectionOutputPort
    {
        public CreateCollectionOutputPort()
        {
            UserHasAlreadyOneCollectionMethod = MethodInvocation<Owner>.NotInvoked(nameof(UserHasAlreadyOneCollection));
        }

        private MethodInvocation<Owner> UserHasAlreadyOneCollectionMethod { set; get; }

        public void UserHasAlreadyOneCollection(Owner owner)
        {
            this.UserHasAlreadyOneCollectionMethod = UserHasAlreadyOneCollectionMethod.Invoked(owner);
        }

        public void ShouldHaveUserHasAlreadyOneCollectionMessage(Owner expectedOwner)
        {
            this.UserHasAlreadyOneCollectionMethod.ShouldBeInvokedWithTheArgument(expectedOwner);
        }
    }
}