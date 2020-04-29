using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerInputValidator : AbstractValidator<GetCollectionByOwnerInput>
    {
        public GetCollectionByOwnerInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty()
                .NotNull();
        }
    }
}
