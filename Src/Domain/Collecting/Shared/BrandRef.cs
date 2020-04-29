using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public sealed class BrandRef : IBrandRef, IEquatable<BrandRef>
    {
        public BrandRef(string name, Slug slug)
        {
            Name = name;
            Slug = slug;
        }

        public static IBrandRef FromBrandInfo(IBrandInfo info) =>
            new BrandRef(info.ToLabel(), info.Slug);

        public string Name { get; }

        public Slug Slug { get; }

        public bool Equals(BrandRef other)
        {
            throw new NotImplementedException();
        }
    }
}
