using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandInfo
    {
        BrandId BrandId { get; }

        Slug Slug { get; }

        string Name { get; }

        string ToLabel() => Name;
    }
}