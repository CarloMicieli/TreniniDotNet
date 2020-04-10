using FluentValidation;
using System;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
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

            RuleFor(x => x.PeriodOfActivity)
                .SetValidator(new PeriodOfActivityInputValidator());

            RuleFor(x => x.TotalLength)
                .SetValidator(new TotalRailwayLengthInputValidator());

            RuleFor(x => x.Gauge)
                .SetValidator(new RailwayGaugeInputValidator());
        }
    }

    public sealed class RailwayGaugeInputValidator : AbstractValidator<RailwayGaugeInput>
    {
        public RailwayGaugeInputValidator()
        {
            RuleFor(x => x.TrackGauge)
                .IsEnumName(typeof(TrackGauge), caseSensitive: false);

            RuleFor(x => x.Millimeters)
                .GreaterThan(0M);

            RuleFor(x => x.Inches)
                .GreaterThan(0M);
        }
    }

    public sealed class TotalRailwayLengthInputValidator : AbstractValidator<TotalRailwayLengthInput>
    {
        public TotalRailwayLengthInputValidator()
        {
            RuleFor(x => x.Kilometers)
                .GreaterThan(0);

            RuleFor(x => x.Miles)
                .GreaterThan(0);
        }
    }

    public sealed class PeriodOfActivityInputValidator : AbstractValidator<PeriodOfActivityInput>
    {
        public PeriodOfActivityInputValidator()
        {
            RuleFor(x => x.OperatingUntil)
                .GreaterThan(x => x.OperatingSince);

            RuleFor(x => x.Status)
                .IsEnumName(typeof(RailwayStatus), caseSensitive: false);

            RuleFor(x => x.Status)
                .Must((input, status) => ValidateStatusAndOperatingUntil(status, input.OperatingUntil))
                .WithMessage("Invalid railway: operating until must be unset for active railways");
        }

        private static bool ValidateStatusAndOperatingUntil(string? status, DateTime? operatingUntil)
        {
            if (status is null)
                return true;

            string activeStatus = RailwayStatus.Active.ToString().ToLowerInvariant();
            string railwayStatus = status.ToLowerInvariant();

            if (railwayStatus == activeStatus)
            {
                return operatingUntil.HasValue == false;
            }

            return true;
        }
    }
}