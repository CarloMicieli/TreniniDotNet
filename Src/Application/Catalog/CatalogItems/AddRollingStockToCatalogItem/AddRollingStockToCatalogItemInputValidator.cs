using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemInputValidator : AbstractValidator<AddRollingStockToCatalogItemInput>
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
