namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public interface ICreateBrandOutputPort : IOutputPortStandard<CreateBrandOutput>
    {
        void BrandAlreadyExists(string message);
    }
}
