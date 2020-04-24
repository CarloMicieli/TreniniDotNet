using FluentValidation;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem
{
    public sealed class EditCollectionItemInputValidator : AbstractValidator<EditCollectionItemInput>
    {
        public EditCollectionItemInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.ItemId)
                .NotEmpty();

            RuleFor(x => x.Price)
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
