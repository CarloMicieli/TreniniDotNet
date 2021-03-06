using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Catalog;
using TreniniDotNet.GrpcServices.Infrastructure;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.GrpcServices.Catalog.Brands
{
    public sealed class CreateBrandPresenter : DefaultGrpcPresenter<CreateBrandOutput, CreateBrandResponse>, ICreateBrandOutputPort
    {
        public CreateBrandPresenter()
            : base(Mapping)
        {
        }

        private static CreateBrandResponse Mapping(CreateBrandOutput output) =>
            new CreateBrandResponse { Slug = output.Slug.Value };

        public void BrandAlreadyExists(Slug brand)
            => AlreadyExists(brand.ToString());
    }
}
