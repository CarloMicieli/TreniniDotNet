using System;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public static class NewPeriodOfActivityInput
    {
        public static PeriodOfActivityInput With(
            string status = null,
            DateTime? operatingUntil = null,
            DateTime? operatingSince = null) =>
            new PeriodOfActivityInput(status, operatingSince, operatingUntil);
    }
}
