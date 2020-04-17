using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public interface IBrandRef
    {
        string Name { get; }
        Slug Slug { get; }
    }
}
