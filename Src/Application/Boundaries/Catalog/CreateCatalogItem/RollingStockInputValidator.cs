using FluentValidation;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class RollingStockInputValidator : AbstractValidator<RollingStockInput>
    {
        public RollingStockInputValidator()
        {
            RuleFor(x => x.Era)
                .NotNull()
                .IsEnumName(typeof(Era), caseSensitive: false);

            RuleFor(x => x.Category)
                .NotNull()
                .IsEnumName(typeof(Category), caseSensitive: false);

            RuleFor(x => x.Length)
                .GreaterThanOrEqualTo(0M);

            RuleFor(x => x.Railway)
                .NotEmpty()
                .NotNull()
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
        }
    }
}