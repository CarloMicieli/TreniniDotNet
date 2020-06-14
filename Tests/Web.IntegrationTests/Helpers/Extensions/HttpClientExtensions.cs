using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Helpers.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TContent> GetJsonAsync<TContent>(this HttpClient httpClient, string requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode == false)
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var error = await response.ExtractContent<ErrorResponse>();
                    throw new HttpAssertException(error);
                }

                response.EnsureSuccessStatusCode();
            }

            return await response.ExtractContent<TContent>();
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(this HttpClient httpClient, string requestUri, object model, Check check)
        {
            HttpContent content = JsonContent(model);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
            return await CheckResponse(response, check);
        }

        public static async Task<HttpResponseMessage> PutJsonAsync(this HttpClient httpClient, string requestUri, object model, Check check)
        {
            HttpContent content = JsonContent(model);
            HttpResponseMessage response = await httpClient.PutAsync(requestUri, content);
            return await CheckResponse(response, check);
        }

        public static async Task<HttpResponseMessage> PatchJsonAsync(this HttpClient httpClient, string requestUri, object model, Check check)
        {
            HttpContent content = JsonContent(model);
            HttpResponseMessage response = await httpClient.PatchAsync(requestUri, content);
            return await CheckResponse(response, check);
        }

        public static async Task<HttpResponseMessage> DeleteJsonAsync(this HttpClient httpClient, string requestUri, Check check)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(requestUri);
            return await CheckResponse(response, check);
        }

        private static async Task<HttpResponseMessage> CheckResponse(HttpResponseMessage response, Check check)
        {
            if (check == Check.IsSuccessful && !response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var badRequest = await response.ExtractContent<BadRequestResponse>();
                    if (badRequest.Errors is null)
                    {
                        var s = await response.Content.ReadAsStringAsync();
                        throw new AssertionFailedException(s);
                    }
                    else
                    {
                        throw new HttpAssertException(badRequest);
                    }
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

        public static async Task<string> GenerateJwtTokenAsync(this HttpClient http, string username, string password)
        {
            var login = new
            {
                username,
                password
            };

            var request = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("/api/v1/Authenticate", request);

            if (response.IsSuccessStatusCode)
            {
                var auth = await response.ExtractContent<Authentication>();
                return auth.Token;
            }

            return "Invalid_Token";
        }

        private class Authentication
        {
            public string Token { set; get; }
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
