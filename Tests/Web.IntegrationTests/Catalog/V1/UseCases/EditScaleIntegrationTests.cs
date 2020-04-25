using IntegrationTests;
using TreniniDotNet.Web;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public class EditScaleIntegrationTests : AbstractWebApplicationFixture
    {
        public EditScaleIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }
    }
}
