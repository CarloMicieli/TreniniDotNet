using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public sealed class CreateRailwayOutputPort : OutputPortTestHelper<CreateRailwayOutput>, ICreateRailwayOutputPort
    {
        private MethodInvocation<Slug> RailwayAlreadyExistsMethod { set; get; }

        public CreateRailwayOutputPort()
        {
            RailwayAlreadyExistsMethod = NewMethod<Slug>(nameof(RailwayAlreadyExists));
        }

        public void RailwayAlreadyExists(Slug railway)
        {
            this.RailwayAlreadyExistsMethod = this.RailwayAlreadyExistsMethod.Invoked(railway);
        }

        public void AssertRailwayAlreadyExists(Slug expectedSlug)
        {
            this.RailwayAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedSlug);
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
