using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
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
