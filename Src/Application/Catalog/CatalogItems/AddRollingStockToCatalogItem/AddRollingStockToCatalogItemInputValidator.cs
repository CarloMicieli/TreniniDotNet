using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemInputValidator : AbstractUseCaseValidator<AddRollingStockToCatalogItemInput>
    {
        public AddRollingStockToCatalogItemInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();

            RuleFor(x => x.RollingStock)
                .SetValidator(new RollingStockInputValidator());
        }
    }
}
