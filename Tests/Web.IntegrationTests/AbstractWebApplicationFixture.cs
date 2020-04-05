using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
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

        protected List<object> JsonArray(object element)
        {
            return new List<object>() { element };
        }

        protected List<object> JsonArray(params object[] elements)
        {
            return new List<object>(elements);
        }

        protected HttpContent JsonContent(object model)
        {
            return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }

        protected async Task<TContent> ExtractContent<TContent>(HttpResponseMessage response)
        {
            var s = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TContent>(s, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        protected Task<TContent> GetJsonAsync<TContent>(string requestUri)
        {
            return CreateHttpClient().GetJsonAsync<TContent>(requestUri);
        }

        protected Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return CreateHttpClient().GetAsync(requestUri);
        }
    }
}