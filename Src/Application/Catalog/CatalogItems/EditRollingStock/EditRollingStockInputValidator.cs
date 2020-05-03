using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockInputValidator : AbstractValidator<EditRollingStockInput>
    {
        public EditRollingStockInputValidator()
        {
            RuleFor(x => x.RollingStockId)
                .NotEmpty();

            RuleFor(x => x.Slug)
                .ValidSlug();
        }
    }
}
