using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsUseCase : IGetLatestCatalogItemsUseCase
    {
        private readonly CatalogItemService _catalogItemService;

        public GetLatestCatalogItemsUseCase(CatalogItemService catalogItemService, IGetLatestCatalogItemsOutputPort outputPort)
        {
            OutputPort = outputPort;
            _catalogItemService = catalogItemService;
        }

        public IGetLatestCatalogItemsOutputPort OutputPort { get; }

        public async Task Execute(GetLatestCatalogItemsInput input)
        {
            var paginatedResult = await _catalogItemService.GetLatestCatalogItemsAsync(input.Page);
            OutputPort.Standard(new GetLatestCatalogItemsOutput(paginatedResult));
        }
    }
}
