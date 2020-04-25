using IntegrationTests;
using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditCatalogItemIntegrationTests : AbstractWebApplicationFixture
    {
        public EditCatalogItemIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}
