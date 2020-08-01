using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Uploads.V1
{
    public class GetImageByFilenameIntegrationTests : AbstractWebApplicationFixture
    {
        public GetImageByFilenameIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetImageByFilename_ShouldReturn200Ok_ReturnTheImage()
        {
            var client = await CreateAuthorizedHttpClientAsync();

            byte[] imageData = default(byte[]);
            using var reader = new StreamReader(@"../../../Uploads/image.jpg");
            using var memoryStream = new MemoryStream();

            reader.BaseStream.CopyTo(memoryStream);
            imageData = memoryStream.ToArray();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(imageContent, "file", "image.jpg");

            var response = await client.PostAsync("/api/v1/images", requestContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.ExtractContent<ImageResponse>();

            var filename = content.Filename;

            var imageResponse = await client.GetAsync($"/api/v1/images/{filename}");
            imageResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
