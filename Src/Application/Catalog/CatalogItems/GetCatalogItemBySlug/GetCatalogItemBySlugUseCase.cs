using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public class GetCatalogItemBySlugUseCase : AbstractUseCase<GetCatalogItemBySlugInput, GetCatalogItemBySlugOutput, IGetCatalogItemBySlugOutputPort>
    {
        private readonly CatalogItemsService _catalogItemService;

        public GetCatalogItemBySlugUseCase(
            IUseCaseInputValidator<GetCatalogItemBySlugInput> inputValidator,
            IGetCatalogItemBySlugOutputPort outputPort,
            CatalogItemsService catalogItemService)
            : base(inputValidator, outputPort)
        {
            _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        }

        protected override async Task Handle(GetCatalogItemBySlugInput input)
        {
            var item = await _catalogItemService.GetBySlugAsync(input.Slug);
            if (item is null)
            {
                OutputPort.CatalogItemNotFound($"The catalog item '{input.Slug}' was not found");
                return;
            }

            OutputPort.Standard(new GetCatalogItemBySlugOutput(item));
        }
    }
}
