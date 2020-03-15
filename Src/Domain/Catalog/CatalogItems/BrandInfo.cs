using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class BrandInfo : IBrandInfo
    {
        private readonly BrandId brandId;
        private readonly Slug slug;
        private readonly string name;

        public BrandInfo(Guid brandId, string slug, string name)
            : this(new BrandId(brandId), Slug.Of(slug), name)
        {
        }

        public BrandInfo(BrandId brandId, Slug slug, string name)
        {
            this.brandId = brandId;
            this.slug = slug;
            this.name = name;
        }

        public BrandId BrandId => brandId;

        public Slug Slug => slug;

        public string Name => name;

        public IBrandInfo ToBrandInfo()
        {
            return this;
        }
    }
}
