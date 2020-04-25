using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
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
