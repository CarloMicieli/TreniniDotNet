using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
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
