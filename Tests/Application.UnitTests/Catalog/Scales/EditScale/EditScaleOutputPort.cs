using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Scales.OutputPorts
{
    public class EditScaleOutputPort : OutputPortTestHelper<EditScaleOutput>, IEditScaleOutputPort
    {
        private MethodInvocation<Slug> ScaleNotFoundMethod { set; get; }

        public EditScaleOutputPort()
        {
            ScaleNotFoundMethod = NewMethod<Slug>(nameof(ScaleNotFound));
        }

        public void ScaleNotFound(Slug slug)
        {
            ScaleNotFoundMethod = ScaleNotFoundMethod.Invoked(slug);
        }

        public void AssertScaleWasNotFound(Slug expectedSlug) =>
            ScaleNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    ScaleNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
