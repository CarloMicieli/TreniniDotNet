namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public interface IOutputPort
        : IOutputPortStandard<CreateBrandOutput>
    {
        void BrandAlreadyExists(string message);
    }
}
