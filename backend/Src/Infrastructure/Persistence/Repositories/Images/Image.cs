namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Images
{
#pragma warning disable CA1819 // Properties should not return arrays
    public sealed class Image
    {
        public Image(string filename, string contentType, byte[] content)
        {
            Filename = filename;
            ContentType = contentType;
            Content = content;
        }

        public string Filename { get; }
        public string ContentType { get; }
        public byte[] Content { get; }
    }
#pragma warning restore CA1819 // Properties should not return arrays
}
