using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemInputValidator : AbstractUseCaseValidator<EditCatalogItemInput>
    {
        public EditCatalogItemInputValidator()
        {
            RuleFor(x => x.Slug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedCatalogItemValuesValidator());
        }
    }

    public sealed class ModifiedCatalogItemValuesValidator : AbstractValidator<ModifiedCatalogItemValues>
    {
        public ModifiedCatalogItemValuesValidator()
        {
            RuleFor(x => x.ItemNumber)
                .MinimumLength(4)
                .MaximumLength(10);

            RuleFor(x => x.Description)
                .MaximumLength(250);

            RuleFor(x => x.PrototypeDescription)
                .MaximumLength(2500);

            RuleFor(x => x.ModelDescription)
                .MaximumLength(2500);

            RuleFor(x => x.PowerMethod)
                .IsEnumName(typeof(PowerMethod), caseSensitive: false);

            RuleFor(x => x.Scale)
                .MaximumLength(10);

            RuleForEach(x => x.RollingStocks)
                .SetValidator(new RollingStockInputValidator());
        }
    }
}
