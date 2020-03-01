namespace TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug
{
    public interface IGetCatalogItemBySlugOutputPort : IOutputPortStandard<GetCatalogItemBySlugOutput>
    {
        void CatalogItemNotFound(string message);
    }
}
