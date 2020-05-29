using FluentValidation;
using TreniniDotNet.Common.Validation;
using TreniniDotNet.Domain.Catalog.CatalogItems;

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

            RuleFor(x => x.Values)
                .SetValidator(new RollingStockModifiedValuesValidator());
        }
    }

    public sealed class RollingStockModifiedValuesValidator : AbstractValidator<RollingStockModifiedValues>
    {
        public RollingStockModifiedValuesValidator()
        {
            RuleFor(x => x.Epoch)
                .ValidEpoch();

            RuleFor(x => x.Category)
                .IsEnumName(typeof(Category), caseSensitive: false);

            RuleFor(x => x.LengthOverBuffer)
                .ValidLengthOverBuffer();

            RuleFor(x => x.Railway)
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(x => x.RoadNumber)
                .MaximumLength(25);

            RuleFor(x => x.ClassName)
                .MaximumLength(25);

            RuleFor(x => x.DccInterface)
                .IsEnumName(typeof(DccInterface), caseSensitive: false);

            RuleFor(x => x.Control)
                .IsEnumName(typeof(Control), caseSensitive: false);

            RuleFor(x => x.Couplers)
                .IsEnumName(typeof(Couplers), caseSensitive: false);
        }
    }
}
