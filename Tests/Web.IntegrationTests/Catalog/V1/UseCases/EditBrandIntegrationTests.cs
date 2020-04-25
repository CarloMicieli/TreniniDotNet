using IntegrationTests;
using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditBrandIntegrationTests : AbstractWebApplicationFixture
    {
        public EditBrandIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}
