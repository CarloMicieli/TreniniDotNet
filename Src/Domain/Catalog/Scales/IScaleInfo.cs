using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScaleInfo
    {
        ScaleId Id { get; }

        Slug Slug { get; }

        string Name { get; }

        Ratio Ratio { get; }

        string ToLabel() => $"{Name} ({Ratio})";
    }
}
