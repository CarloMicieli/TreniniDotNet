using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests
{
    public abstract class AbstractWebApplicationFixture : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        protected AbstractWebApplicationFixture(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient() => _factory.CreateClient();

        protected async Task<HttpClient> CreateHttpClientAsync(string username, string password)
        {
            var client = _factory.CreateClient();

            var token = await client.GenerateJwtTokenAsync(username, password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        protected Task<HttpClient> CreateAuthorizedHttpClientAsync() =>
            CreateHttpClientAsync("George", "Pa$$word88");

        protected List<object> JsonArray(object element) => new List<object>() { element };

        protected List<object> JsonArray(params object[] elements) => new List<object>(elements);

        protected HttpContent JsonContent(object model) =>
            new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

        protected async Task<TContent> ExtractContent<TContent>(HttpResponseMessage response)
        {
            var s = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TContent>(s, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
