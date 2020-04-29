using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public sealed class CreateCollectionInputValidator : AbstractValidator<CreateCollectionInput>
    {
        public CreateCollectionInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Notes)
                .MaximumLength(150);
        }
    }
}
