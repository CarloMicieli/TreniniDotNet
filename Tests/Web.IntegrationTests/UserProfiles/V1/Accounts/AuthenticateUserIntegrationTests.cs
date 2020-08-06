using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.UserProfiles.V1.Accounts
{
    public class AuthenticateUserIntegrationTests : AbstractWebApplicationFixture
    {
        private readonly HttpClient _client;

        public AuthenticateUserIntegrationTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnJwtTokens()
        {
            var login = new
            {
                username = "Ciccins",
                password = "Pa$$word88"
            };

            var response = await _client.PostJsonAsync("/api/v1/Authenticate", login, Check.IsSuccessful);

            var auth = await response.ExtractContent<Authentication>();
            auth.Token.Should().NotBeEmpty();
        }

        private class Authentication
        {
            public string Token { set; get; }
        }
    }
}