using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public interface ICreateBrandOutputPort : IOutputPortStandard<CreateBrandOutput>
    {
        void BrandAlreadyExists(string message);
    }
}
