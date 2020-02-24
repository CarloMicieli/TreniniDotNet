using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public sealed class CreateRailwayOutputPort : OutputPortTestHelper<CreateRailwayOutput>, ICreateRailwayOutputPort
    {
        private MethodInvocation<string> RailwayAlreadyExistsMethod { set; get; }

        public CreateRailwayOutputPort()
        {
            RailwayAlreadyExistsMethod = MethodInvocation<string>.NotInvoked(nameof(RailwayAlreadyExists));
        }
        
        public void RailwayAlreadyExists(string message)
        {
            this.RailwayAlreadyExistsMethod = this.RailwayAlreadyExistsMethod.Invoked(message);
        }

        public void ShouldHaveRailwayAlreadyExistsMessage(string expectedMessage)
        {
            this.RailwayAlreadyExistsMethod.InvokedWithArgument(expectedMessage);
        }
    }
}
