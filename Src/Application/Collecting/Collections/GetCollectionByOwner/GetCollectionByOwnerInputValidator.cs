using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerInputValidator : AbstractUseCaseValidator<GetCollectionByOwnerInput>
    {
        public GetCollectionByOwnerInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty()
                .NotNull();
        }
    }
}
