using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TreniniDotNet.Infrastructure.Persistence.Images;

namespace TreniniDotNet.Web.Uploads.V1.Images
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ImagesRepository Images { get; }
        private UploadSettings UploadSettings { get; }

        public ImagesController(IOptions<UploadSettings> uploadSettings, ImagesRepository images)
        {
            Images = images;
            UploadSettings = uploadSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync([FromForm] ImageUpload imageUpload)
        {
            if (!IsPermittedExtensions(imageUpload.File.FileName))
            {
                ModelState.AddModelError("File", $"The request couldn't be processed (Invalid file extension).");
                return BadRequest(ModelState);
            }

            await using var memoryStream = new MemoryStream();
            await imageUpload.File.CopyToAsync(memoryStream);

            if (!HasValidFileSignature(imageUpload.File.FileName, memoryStream))
            {
                ModelState.AddModelError("File", $"The request couldn't be processed (Invalid file signature).");
                return BadRequest(ModelState);
            }

            if (memoryStream.Length > UploadSettings.MaxSize)
            {
                ModelState.AddModelError("File", $"The request couldn't be processed (Invalid size).");
                return BadRequest(ModelState);
            }

            string trustedFilenameForStorage = Path.GetRandomFileName();
            string encodedFilename = WebUtility.HtmlEncode(imageUpload.File.FileName);

            var newImage = new Image(trustedFilenameForStorage, imageUpload.File.ContentType, memoryStream.ToArray());
            await Images.SaveImage(newImage);

            return Ok(new
            {
                Filename = trustedFilenameForStorage,
                OriginalFilename = encodedFilename,
                imageUpload.Tag
            });
        }

        [AllowAnonymous]
        [HttpGet("{filename}", Name = "GetImage")]
        public async Task<IActionResult> GetImageByFilename(string filename)
        {
            var image = await Images.GetImageByFilename(filename);
            if (image is null)
            {
                return NotFound();
            }

            return File(image.Content, image.ContentType);
        }

        private bool IsPermittedExtensions(string uploadedFileName)
        {
            var ext = Path.GetExtension(uploadedFileName).ToLowerInvariant();
            return !string.IsNullOrWhiteSpace(ext) && UploadSettings.PermittedExtensions.Contains(ext);
        }

        private static bool HasValidFileSignature(string uploadedFileName, Stream uploadedFileData)
        {
            var ext = Path.GetExtension(uploadedFileName).ToLowerInvariant();
            if (FileSignature.TryGetValue(ext, out var signatures))
            {
                using (var reader = new BinaryReader(uploadedFileData))
                {
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
                    return signatures.Any(signature =>
                        headerBytes.Take(signature.Length).SequenceEqual(signature));
                }
            }

            return true;
        }

        private static readonly Dictionary<string, List<byte[]>> FileSignature =
            new Dictionary<string, List<byte[]>>
            {
                { ".jpeg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    }
                }
            };
    }

    public class ImageUpload
    {
        [Required]
        public IFormFile File { set; get; } = null!;

        public string? Tag { set; get; }
    }
}
