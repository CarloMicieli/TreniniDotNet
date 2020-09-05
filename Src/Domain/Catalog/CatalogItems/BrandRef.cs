using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class BrandRef : AggregateRootRef<Brand, BrandId>
    {
        public BrandRef(BrandId id, string slug, string name)
            : base(id, slug, name)
        {
        }

        public BrandRef(Brand brand)
            : this(brand.Id, brand.Slug.ToString(), brand.Name)
        {
        }

        public static BrandRef? AsOptional(Brand? brand) =>
            (brand is null) ? null : new BrandRef(brand);
    }
}