using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public sealed class GetScaleBySlugOutputPort : OutputPortTestHelper<GetScaleBySlugOutput>, IGetScaleBySlugOutputPort
    {
        private MethodInvocation<string> ScaleNotFoundMethod { set; get; }

        public GetScaleBySlugOutputPort()
        {
            this.ScaleNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(ScaleNotFound));
        }

        public void ScaleNotFound(string message)
        {
            this.ScaleNotFoundMethod = this.ScaleNotFoundMethod.Invoked(message);
        }

        public void ShouldHaveScaleNotFoundMessage(string expectedMessage)
        {
            this.ScaleNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }
    }
}
