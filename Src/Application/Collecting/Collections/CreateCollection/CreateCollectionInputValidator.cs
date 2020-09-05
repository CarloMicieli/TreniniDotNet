using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public sealed class CreateCollectionInputValidator : AbstractUseCaseValidator<CreateCollectionInput>
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
