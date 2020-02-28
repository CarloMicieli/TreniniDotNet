using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public sealed class GetRailwayBySlugOutputPort : OutputPortTestHelper<GetRailwayBySlugOutput>, IGetRailwayBySlugOutputPort
    {
        private MethodInvocation<string> RailwayNotFoundMethod { set; get; }

        public GetRailwayBySlugOutputPort()
        {
            this.RailwayNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(RailwayNotFound));
        }

        public void RailwayNotFound(string message)
        {
            this.RailwayNotFoundMethod = this.RailwayNotFoundMethod.Invoked(message);
        }

        public void ShouldHaveRailwayNotFoundMessage(string expectedMessage)
        {
            this.RailwayNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }
    }
}
