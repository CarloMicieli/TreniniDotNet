using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionInputValidator : AbstractValidator<RemoveItemFromCollectionInput>
    {
        public RemoveItemFromCollectionInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.ItemId)
                .NotEmpty();

            RuleFor(x => x.Owner)
                .NotNull();
        }
    }
}
