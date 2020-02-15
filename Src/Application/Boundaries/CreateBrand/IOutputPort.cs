namespace TreniniDotNet.Application.Boundaries.CreateBrand
{
    public interface IOutputPort
        : IOutputPortStandard<CreateBrandOutput>
    {
        void BrandAlreadyExists(string message);
    }
}
