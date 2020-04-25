using IntegrationTests;
using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditRailwayIntegrationTests : AbstractWebApplicationFixture
    {
        public EditRailwayIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}
