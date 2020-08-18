using FluentValidation;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionInputValidator : AbstractUseCaseValidator<AddItemToCollectionInput>
    {
        public AddItemToCollectionInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty();

            RuleFor(x => x.CatalogItem)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Price)
                .NotNull()
                .SetValidator(new PriceInputValidator());

            RuleFor(x => x.Condition)
                .IsEnumName(typeof(Condition), caseSensitive: false);

            RuleFor(x => x.Notes)
                .MaximumLength(150);

            RuleFor(x => x.Shop)
                .MaximumLength(50);
        }
    }
}
