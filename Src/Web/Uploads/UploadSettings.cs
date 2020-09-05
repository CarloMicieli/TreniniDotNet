using System.Collections.Generic;

namespace TreniniDotNet.Web.Uploads
{
    public sealed class UploadSettings
    {
        public int MaxSize { set; get; } = 1024 * 256;
        public List<string> PermittedExtensions { set; get; } = new List<string>();
    }
}
