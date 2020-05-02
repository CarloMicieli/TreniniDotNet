using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
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
