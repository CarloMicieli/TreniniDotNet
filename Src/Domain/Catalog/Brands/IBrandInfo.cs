using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandInfo
    {
        Slug Slug { get; }

        string Name { get; }

        string ToLabel() => Name;

        IBrandInfo ToBrandInfo();
    }
}