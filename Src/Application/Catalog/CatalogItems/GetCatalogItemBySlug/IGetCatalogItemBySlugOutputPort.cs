using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public interface IGetCatalogItemBySlugOutputPort : IOutputPortStandard<GetCatalogItemBySlugOutput>
    {
        void CatalogItemNotFound(string message);
    }
}
