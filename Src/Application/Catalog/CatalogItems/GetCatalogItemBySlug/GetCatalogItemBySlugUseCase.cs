using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public class GetCatalogItemBySlugUseCase : IGetCatalogItemBySlugUseCase
    {
        private readonly CatalogItemService _catalogItemService;

        public GetCatalogItemBySlugUseCase(CatalogItemService catalogItemService, IGetCatalogItemBySlugOutputPort outputPort)
        {
            OutputPort = outputPort;
            _catalogItemService = catalogItemService;
        }

        public IGetCatalogItemBySlugOutputPort OutputPort { get; }

        public async Task Execute(GetCatalogItemBySlugInput input)
        {
            ICatalogItem? item = await _catalogItemService.GetBySlugAsync(input.Slug);
            if (item is null)
            {
                OutputPort.CatalogItemNotFound($"The catalog item '{input.Slug}' was not found");
                return;
            }

            OutputPort.Standard(new GetCatalogItemBySlugOutput(item));
        }
    }
}
