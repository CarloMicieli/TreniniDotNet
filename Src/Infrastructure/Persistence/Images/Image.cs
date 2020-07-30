using System;

namespace TreniniDotNet.Infrastructure.Persistence.Images
{
#pragma warning disable CA1819 // Properties should not return arrays
    public sealed class Image
    {
        private Image() {}

        public Image(string filename, string contentType, byte[] content)
        {
            Filename = filename;
            ContentType = contentType;
            Content = content;
            IsDeleted = false;
            CreatedDate = DateTime.UtcNow;
        }

        public string Filename { get; } = null!;
        public string ContentType { get; } = null!;
        public byte[] Content { get; } = null!;
        public bool? IsDeleted { get; }
        public DateTime CreatedDate { get; }
    }
#pragma warning restore CA1819 // Properties should not return arrays
}
