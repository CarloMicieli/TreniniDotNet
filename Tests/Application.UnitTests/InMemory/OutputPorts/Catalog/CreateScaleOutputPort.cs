using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public class CreateScaleOutputPort : OutputPortTestHelper<CreateScaleOutput>, ICreateScaleOutputPort
    {
        private MethodInvocation<string> ScaleAlreadyExistsMethod { set; get; }

        public CreateScaleOutputPort()
        {
            ScaleAlreadyExistsMethod = MethodInvocation<string>.NotInvoked(nameof(ScaleAlreadyExists));
        }

        public void ShouldHaveScaleAlreadyExistsMessage(string expectedMessage)
        {
            this.ScaleAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void ScaleAlreadyExists(string message)
        {
            this.ScaleAlreadyExistsMethod = this.ScaleAlreadyExistsMethod.Invoked(message);
        }
    }
}
