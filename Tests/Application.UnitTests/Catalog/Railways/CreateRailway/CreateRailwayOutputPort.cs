using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Railways.OutputPorts
{
    public sealed class CreateRailwayOutputPort : OutputPortTestHelper<CreateRailwayOutput>, ICreateRailwayOutputPort
    {
        private MethodInvocation<string> RailwayAlreadyExistsMethod { set; get; }

        public CreateRailwayOutputPort()
        {
            RailwayAlreadyExistsMethod = NewMethod<string>(nameof(RailwayAlreadyExists));
        }

        public void RailwayAlreadyExists(string message)
        {
            this.RailwayAlreadyExistsMethod = this.RailwayAlreadyExistsMethod.Invoked(message);
        }

        public void ShouldHaveRailwayAlreadyExistsMessage(string expectedMessage)
        {
            this.RailwayAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    RailwayAlreadyExistsMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
