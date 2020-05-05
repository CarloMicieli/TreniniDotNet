using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public class CreateScaleOutputPort : OutputPortTestHelper<CreateScaleOutput>, ICreateScaleOutputPort
    {
        private MethodInvocation<Slug> ScaleAlreadyExistsMethod { set; get; }

        public CreateScaleOutputPort()
        {
            ScaleAlreadyExistsMethod = NewMethod<Slug>(nameof(ScaleAlreadyExists));
        }

        public void AssertScaleAlreadyExists(Slug expectedSlug)
        {
            this.ScaleAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedSlug);
        }

        public void ScaleAlreadyExists(Slug scaleSlug)
        {
            this.ScaleAlreadyExistsMethod = this.ScaleAlreadyExistsMethod.Invoked(scaleSlug);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    ScaleAlreadyExistsMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
