using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemInputValidator : AbstractUseCaseValidator<RemoveRollingStockFromCatalogItemInput>
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
