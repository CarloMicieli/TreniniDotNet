using FluentValidation;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem
{
    public sealed class EditCatalogItemInputValidator : AbstractValidator<EditCatalogItemInput>
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
