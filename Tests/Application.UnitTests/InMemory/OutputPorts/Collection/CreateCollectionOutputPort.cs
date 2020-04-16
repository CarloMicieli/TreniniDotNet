using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class CreateCollectionOutputPort : OutputPortTestHelper<CreateCollectionOutput>, ICreateCollectionOutputPort
    {
        public CreateCollectionOutputPort()
        {
            UserHasAlreadyOneCollectionMethod = MethodInvocation<string>.NotInvoked(nameof(UserHasAlreadyOneCollection));
        }

        private MethodInvocation<string> UserHasAlreadyOneCollectionMethod { set; get; }

        public void UserHasAlreadyOneCollection(string message)
        {
            this.UserHasAlreadyOneCollectionMethod = UserHasAlreadyOneCollectionMethod.Invoked(message);
        }

        public void ShouldHaveUserHasAlreadyOneCollectionMessage(string expectedMessage)
        {
            this.UserHasAlreadyOneCollectionMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }
    }
}
