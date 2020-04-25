using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.UseCases.Catalog.CatalogItems
{
    public class GetCatalogItemBySlug : IGetCatalogItemBySlugUseCase
    {
        private readonly IGetCatalogItemBySlugOutputPort _outputPort;
        private readonly CatalogItemService _catalogItemService;

        public GetCatalogItemBySlug(CatalogItemService catalogItemService, IGetCatalogItemBySlugOutputPort outputPort)
        {
            _outputPort = outputPort;
            _catalogItemService = catalogItemService;
        }

        public IGetCatalogItemBySlugOutputPort OutputPort => _outputPort;

        public async Task Execute(GetCatalogItemBySlugInput input)
        {
            ICatalogItem? item = await _catalogItemService.FindBySlug(input.Slug);
            if (item is null)
            {
                OutputPort.CatalogItemNotFound($"The catalog item '{input.Slug}' was not found");
                return;
            }

            OutputPort.Standard(new GetCatalogItemBySlugOutput(item));
        }
    }
}
