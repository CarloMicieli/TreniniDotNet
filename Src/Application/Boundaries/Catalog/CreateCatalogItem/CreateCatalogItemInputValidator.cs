using FluentValidation;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemInputValidator : AbstractValidator<CreateCatalogItemInput>
    {
        public CreateCatalogItemInputValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ItemNumber)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(10);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(250);

            RuleFor(x => x.PrototypeDescription)
                .MaximumLength(2500);

            RuleFor(x => x.ModelDescription)
                .MaximumLength(2500);

            RuleFor(x => x.PowerMethod)
                .NotNull()
                .IsEnumName(typeof(PowerMethod), caseSensitive: false);

            RuleFor(x => x.Scale)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);

            RuleFor(x => x.RollingStocks)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.RollingStocks)
                .SetValidator(new RollingStockInputValidator());
        }
    }
}