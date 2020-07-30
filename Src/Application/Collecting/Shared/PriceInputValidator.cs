using FluentValidation;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Shared
{
    public sealed class PriceInputValidator : AbstractValidator<PriceInput>
    {
        public PriceInputValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0.1M);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Must(Price.IsValidCurrency).WithMessage("Invalid currency code");
        }
    }
}
