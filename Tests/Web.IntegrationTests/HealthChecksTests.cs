using System.Net.Http;
using System.Threading.Tasks;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests
{
    public class HealthChecksTests : AbstractWebApplicationFixture
    {
        private HttpClient _client;

        public HealthChecksTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task HealthChecks()
        {
            var response = await _client.GetAsync("/health");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
