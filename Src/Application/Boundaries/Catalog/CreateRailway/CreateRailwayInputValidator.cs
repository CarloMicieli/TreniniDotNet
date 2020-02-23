using FluentValidation;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public sealed class CreateRailwayInputValidator : AbstractValidator<CreateRailwayInput>
    {
        public CreateRailwayInputValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(x => x.Country)
                .CountryCode();

            RuleFor(x => x.OperatingUntil)
                .GreaterThan(x => x.OperatingSince);

            RuleFor(x => x.Status)
                .IsEnumName(typeof(RailwayStatus), caseSensitive: false);

            RuleFor(x => x.Status)
                .Must((input, status) => RailwayStatus.Active.ToString() == status && input.OperatingUntil.HasValue == false)
                .WithMessage("Invalid railway: operating until must be unset for active railways");
        }            
    }
}
