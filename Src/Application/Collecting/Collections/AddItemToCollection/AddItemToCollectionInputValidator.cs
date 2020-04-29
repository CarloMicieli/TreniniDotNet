using FluentValidation;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionInputValidator : AbstractValidator<AddItemToCollectionInput>
    {
        public AddItemToCollectionInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty();

            RuleFor(x => x.CatalogItem)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0M);

            RuleFor(x => x.Condition)
                .IsEnumName(typeof(Condition), caseSensitive: false);

            RuleFor(x => x.Notes)
                .MaximumLength(150);

            RuleFor(x => x.Shop)
                .MaximumLength(50);
        }
    }
}