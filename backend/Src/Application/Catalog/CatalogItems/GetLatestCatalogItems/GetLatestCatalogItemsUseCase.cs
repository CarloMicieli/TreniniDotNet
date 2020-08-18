using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsUseCase : AbstractUseCase<GetLatestCatalogItemsInput, GetLatestCatalogItemsOutput, IGetLatestCatalogItemsOutputPort>
    {
        private readonly CatalogItemsService _catalogItemService;

        public GetLatestCatalogItemsUseCase(
            IUseCaseInputValidator<GetLatestCatalogItemsInput> inputValidator,
            IGetLatestCatalogItemsOutputPort outputPort,
            CatalogItemsService catalogItemService)
            : base(inputValidator, outputPort)
        {
            _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        }

        protected override async Task Handle(GetLatestCatalogItemsInput input)
        {
            var paginatedResult = await _catalogItemService.GetLatestCatalogItemsAsync(input.Page);
            OutputPort.Standard(new GetLatestCatalogItemsOutput(paginatedResult));
        }
    }
}
