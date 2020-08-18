using System;
using FluentValidation;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways
{
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
