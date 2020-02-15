namespace TreniniDotNet.Application.Boundaries.GetBrandBySlug
{
    public interface IOutputPort : IOutputPortStandard<GetBrandBySlugOutput>
    {
        void BrandNotFound(string message);
    }
}
