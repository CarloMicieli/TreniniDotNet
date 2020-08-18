using FluentValidation;

namespace TreniniDotNet.Web.UserProfiles.V1.Accounts.CreateAccount
{
    public class Account
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }

    internal class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(50);

            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(50);

        }
    }
}