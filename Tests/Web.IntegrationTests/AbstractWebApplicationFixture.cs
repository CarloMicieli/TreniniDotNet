using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TreniniDotNet.Infrastructure.Identity;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests
{
    public abstract class AbstractWebApplicationFixture : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly ITokensService _tokensService;
        
        protected AbstractWebApplicationFixture(CustomWebApplicationFactory<Startup> factory)
        {
            _tokensService = new JwtTokensService(new JwtSettings
            {
                Secret = "My super secret secure key",
                Issuer = "http://www.trenini.net",
                Audience = "http://www.trenini.net"
            });
            _factory = factory;
        }

        protected HttpClient CreateHttpClient() => _factory.CreateClient();

        protected HttpClient CreateHttpClient(string username, string password)
        {
            var client = _factory.CreateClient();

            var token = _tokensService.CreateToken(username); // await client.GenerateJwtTokenAsync(username, password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
        
        protected HttpClient CreateAuthorizedHttpClient() =>
            CreateHttpClient("George", "Pa$$word88");


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
