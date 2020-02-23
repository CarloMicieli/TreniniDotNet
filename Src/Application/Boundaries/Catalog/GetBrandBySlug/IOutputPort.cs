namespace TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug
{
    public interface IGetBrandBySlugOutputPort : IOutputPortStandard<GetBrandBySlugOutput>
    {
        void BrandNotFound(string message);
    }
}
