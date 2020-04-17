using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public interface IScaleRef
    {
        string Name { get; }
        Slug Slug { get; }
    }
}
