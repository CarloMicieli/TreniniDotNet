using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Railways.OutputPorts
{
    public class EditRailwayOutputPort : OutputPortTestHelper<EditRailwayOutput>, IEditRailwayOutputPort
    {
        private MethodInvocation<Slug> RailwayNotFoundMethod { set; get; }

        public EditRailwayOutputPort()
        {
            RailwayNotFoundMethod = NewMethod<Slug>(nameof(RailwayNotFound));
        }

        public void RailwayNotFound(Slug slug)
        {
            RailwayNotFoundMethod = RailwayNotFoundMethod.Invoked(slug);
        }

        public void AssertRailwayNotFound(Slug expectedSlug) =>
            RailwayNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    RailwayNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
