using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public interface IScaleRef
    {
        string Name { get; }
        Slug Slug { get; }
    }
}
