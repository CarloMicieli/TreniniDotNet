using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public interface IBrandRef
    {
        string Name { get; }
        Slug Slug { get; }
    }
}
