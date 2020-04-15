using FluentValidation;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateShop
{
    public sealed class CreateShopInputValidator : AbstractValidator<CreateShopInput>
    {
        public CreateShopInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.WebsiteUrl)
                .AbsoluteUri();

            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .PhoneNumber();
        }
    }
}
