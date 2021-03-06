using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemInputValidator : AbstractUseCaseValidator<CreateCatalogItemInput>
    {
        public CreateCatalogItemInputValidator()
        {
            RuleFor(x => x.Brand)
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
