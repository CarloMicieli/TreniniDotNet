using FluentValidation;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public sealed class BudgetInputValidator : AbstractValidator<BudgetInput>
    {
        public BudgetInputValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0.1M);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Must(Budget.IsValidCurrency).WithMessage("Invalid currency code");
        }
    }
}
