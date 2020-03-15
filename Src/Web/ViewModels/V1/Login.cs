using FluentValidation;

namespace TreniniDotNet.Web.ViewModels.V1
{
    public class Login
    {
        public string? Username { set; get; }
        public string? Password { get; set; }
    }

    internal class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}