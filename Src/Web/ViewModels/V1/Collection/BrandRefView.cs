using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class BrandRefView
    {
        private readonly IBrandRef _brand;

        public BrandRefView(IBrandRef brand)
        {
            _brand = brand;
        }

        public string Slug => _brand.Slug;
        public string Value => _brand.Name;
    }
}
