using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner
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
