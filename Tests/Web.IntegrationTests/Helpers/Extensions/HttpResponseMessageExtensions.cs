using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TreniniDotNet.IntegrationTests.Helpers.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TContent> ExtractContent<TContent>(this HttpResponseMessage response)
        {
            var s = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TContent>(s, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static async Task LogAsyncTo(this HttpResponseMessage response, ITestOutputHelper output)
        {
            output?.WriteLine("Response was: {0}", response);
            output?.WriteLine("with content: {0}", await response.Content.ReadAsStringAsync());
        }
    }
}
