using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
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
            this.ScaleNotFoundMethod.InvokedWithArgument(expectedMessage);
        }
    }
}
