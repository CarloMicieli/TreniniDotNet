using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemInputValidator : AbstractValidator<RemoveRollingStockFromCatalogItemInput>
    {
        public RemoveRollingStockFromCatalogItemInputValidator()
        {
            RuleFor(x => x.CatalogItemSlug)
                .ValidSlug();

            RuleFor(x => x.RollingStockId)
                .NotEmpty();
        }
    }
}
