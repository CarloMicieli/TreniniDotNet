using System;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public sealed class PeriodOfActivityInput
    {
        public PeriodOfActivityInput(
            string? status,
            DateTime? operatingSince,
            DateTime? operatingUntil)
        {
            Status = status;
            OperatingUntil = operatingUntil;
            OperatingSince = operatingSince;
        }

        public string? Status { get; }
        public DateTime? OperatingUntil { get; }
        public DateTime? OperatingSince { get; }

        public void Deconstruct(out string? status, out DateTime? since, out DateTime? until)
        {
            status = Status;
            since = OperatingSince;
            until = OperatingUntil;
        }

        public PeriodOfActivity ToPeriodOfActivity()
        {
            var (status, since, until) = this;
            return PeriodOfActivity.Of(status, since, until);
        }

        public static PeriodOfActivityInput Default() =>
            new PeriodOfActivityInput(null, null, null);
    }
}
