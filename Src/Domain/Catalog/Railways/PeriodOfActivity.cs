using System;
using LanguageExt;
using TreniniDotNet.Common;
using static LanguageExt.Prelude;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class PeriodOfActivity
    {
        //TODO: make private!
        internal PeriodOfActivity(DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus status)
        {
            OperatingSince = operatingSince;
            OperatingUntil = operatingUntil;
            RailwayStatus = status;
        }

        public DateTime? OperatingSince { get; }
        public DateTime? OperatingUntil { get; }
        public RailwayStatus RailwayStatus { get; }

        public static PeriodOfActivity ActiveRailway(DateTime operatingSince) =>
            new PeriodOfActivity(operatingSince, null, RailwayStatus.Active);

        public static PeriodOfActivity InactiveRailway(DateTime operatingSince, DateTime operatingUntil) =>
            new PeriodOfActivity(operatingSince, operatingUntil, RailwayStatus.Inactive);

        public static PeriodOfActivity Default() =>
            new PeriodOfActivity(null, null, RailwayStatus.Active);

        public static Validation<Error, PeriodOfActivity> TryCreate(DateTime? operatingSince, DateTime? operatingUntil, bool? active)
        {
            var rs = active == true ? RailwayStatus.Active : RailwayStatus.Inactive;

            if (operatingSince.HasValue && operatingUntil.HasValue)
            {
                if (operatingSince.Value.CompareTo(operatingUntil.Value) > 0)
                {
                    return Error.New(ExceptionMessages.InvalidRailwaySinceLaterThanOperatingUntil);
                }
            }

            if (operatingUntil.HasValue && rs == RailwayStatus.Active)
            {
                return Error.New(ExceptionMessages.InvalidRailwayOperatingUntilForActiveRailway);
            }

            if (!operatingUntil.HasValue && rs == RailwayStatus.Inactive)
            {
                return Error.New(ExceptionMessages.InvalidRailwayOperatingUntilForInactiveRailway);
            }

            return new PeriodOfActivity(operatingSince, operatingUntil, rs);
        }
    }
}