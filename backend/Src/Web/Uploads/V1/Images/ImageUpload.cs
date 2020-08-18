using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TreniniDotNet.Web.Uploads.V1.Images
{
    public sealed class ImageUpload
    {
        [Required]
        public IFormFile File { set; get; } = null!;

        public string? Tag { set; get; }
    }
}
