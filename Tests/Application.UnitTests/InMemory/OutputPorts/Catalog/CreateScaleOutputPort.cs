using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public class CreateScaleOutputPort : OutputPortTestHelper<CreateScaleOutput>, ICreateScaleOutputPort
    {
        private MethodInvocation<string> ScaleAlreadyExistsMethod { set; get; }

        public CreateScaleOutputPort()
        {
            ScaleAlreadyExistsMethod = NewMethod<string>(nameof(ScaleAlreadyExists));
        }

        public void ShouldHaveScaleAlreadyExistsMessage(string expectedMessage)
        {
            this.ScaleAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void ScaleAlreadyExists(string message)
        {
            this.ScaleAlreadyExistsMethod = this.ScaleAlreadyExistsMethod.Invoked(message);
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
