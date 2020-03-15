using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScaleInfo
    {
        Slug Slug { get; }

        string Name { get; }

        Ratio Ratio { get; }

        string ToLabel() => $"{Name} ({Ratio})";

        IScaleInfo ToScaleInfo();
    }
}