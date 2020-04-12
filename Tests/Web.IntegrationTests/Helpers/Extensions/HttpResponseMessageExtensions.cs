using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
    }
}
