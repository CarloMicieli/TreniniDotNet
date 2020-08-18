using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public interface ICreateBrandOutputPort : IStandardOutputPort<CreateBrandOutput>
    {
        void BrandAlreadyExists(Slug brand);
    }
}
