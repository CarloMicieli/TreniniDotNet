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
    public class PostImageIntegrationTests : AbstractWebApplicationFixture
    {
        public PostImageIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PostImage_ShouldReturn200OK_WhenImageWasUploaded()
        {
            var client = CreateAuthorizedHttpClient();

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
            content.Filename.Should().NotBeEmpty();
            content.OriginalFilename.Should().Be("image.jpg");
        }
    }
}
