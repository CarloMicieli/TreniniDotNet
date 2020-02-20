namespace TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug
{
    public interface IOutputPort : IOutputPortStandard<GetBrandBySlugOutput>
    {
        void BrandNotFound(string message);
    }
}
