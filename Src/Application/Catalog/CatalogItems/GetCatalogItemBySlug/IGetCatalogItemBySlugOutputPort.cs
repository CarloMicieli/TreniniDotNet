using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public interface IGetCatalogItemBySlugOutputPort : IStandardOutputPort<GetCatalogItemBySlugOutput>
    {
        void CatalogItemNotFound(string message);
    }
}
