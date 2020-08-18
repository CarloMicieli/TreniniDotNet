#nullable disable
using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.Validation;
using TreniniDotNet.SharedKernel.PhoneNumbers;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public sealed class CreateShopInputValidator : AbstractUseCaseValidator<CreateShopInput>
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
