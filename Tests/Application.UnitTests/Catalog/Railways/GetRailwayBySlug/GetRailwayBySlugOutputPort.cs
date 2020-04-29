using TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Railways.OutputPorts
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
