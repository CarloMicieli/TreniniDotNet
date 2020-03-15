using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Helpers.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostJsonAsync(this HttpClient httpClient, string requestUri, object model, Check check)
        {
            HttpContent content = JsonContent(model);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);

            if (check == Check.IsSuccessful && !response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var badRequest = await response.ExtractContent<BadRequestResponse>();
                    throw new HttpAssertException(badRequest);
                }

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = await response.ExtractContent<ErrorResponse>();
                    throw new HttpAssertException(error);
                }

                response.EnsureSuccessStatusCode();
            }

            return response;
        }

        private static HttpContent JsonContent(object model)
        {
            return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }
    }

    public enum Check
    {
        IsSuccessful,
        Nothing
    }

    public class HttpAssertException : Exception
    {
        public HttpAssertException(ErrorResponse response)
            : base(ToMessage(response))
        {
        }

        public HttpAssertException(BadRequestResponse response)
            : base(ToMessage(response))
        {
        }

        private static string ToMessage(BadRequestResponse r)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Status = {0}, Title = {1}", r.Status, r.Title);
            sb.AppendLine();

            string errors = String.Join(", ", r.Errors.Select(kv => $"{kv.Key}=[{String.Join(", ", kv.Value)}]"));
            sb.Append(errors);

            return sb.ToString();
        }

        private static string ToMessage(ErrorResponse r)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Status = {0}, Title = {1}", r.Status, r.Title);
            sb.AppendLine();

            sb.Append(r.Detail);

            return sb.ToString();
        }
    }
}
