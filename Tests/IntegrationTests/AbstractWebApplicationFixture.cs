using System.Net.Http;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.Web;
using Xunit;

namespace IntegrationTests
{
    public abstract class AbstractWebApplicationFixture : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        
        protected AbstractWebApplicationFixture(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient()
        {
            return _factory.CreateClient();
        }
    }
}