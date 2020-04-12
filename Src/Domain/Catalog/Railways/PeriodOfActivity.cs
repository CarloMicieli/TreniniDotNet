using System;
using System.Diagnostics.CodeAnalysis;
using TreniniDotNet.Common.Enums;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class PeriodOfActivity : IEquatable<PeriodOfActivity>
    {
        private PeriodOfActivity(DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus status)
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

        public static PeriodOfActivity Of(string? status, DateTime? operatingSince, DateTime? operatingUntil)
        {
            if (status is null &&
                operatingSince.HasValue == false &&
                operatingUntil.HasValue == false)
            {
                return Default();
            }

            var rs = EnumHelpers.OptionalValueFor<RailwayStatus>(status) ?? RailwayStatus.Active;
            var (isValid, errorMessage) = Validate(rs, operatingSince, operatingUntil);

            if (isValid)
            {
                return new PeriodOfActivity(operatingSince, operatingUntil, rs);
            }

            throw new InvalidOperationException(errorMessage);
        }

        public static bool TryCreate(string? status, DateTime? operatingSince, DateTime? operatingUntil,
            [NotNullWhen(true)] out PeriodOfActivity? result)
        {
            var rs = EnumHelpers.OptionalValueFor<RailwayStatus>(status) ?? RailwayStatus.Active;
            var (isValid, _) = Validate(rs, operatingSince, operatingUntil);

            if (isValid)
            {
                result = new PeriodOfActivity(operatingSince, operatingUntil, rs);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        private static (bool valid, string? errorMessage) Validate(RailwayStatus rs, DateTime? operatingSince, DateTime? operatingUntil)
        {
            if (operatingSince.HasValue && operatingUntil.HasValue)
            {
                if (operatingSince.Value.CompareTo(operatingUntil.Value) > 0)
                {
                    return (false, "Invalid period of activity: operatingSince > operatingUntil");
                }
            }

            if (operatingUntil.HasValue && rs == RailwayStatus.Active)
            {
                return (false, "Invalid period of activity: operatingUntil has a value for an active railway");
            }

            if (!operatingUntil.HasValue && rs == RailwayStatus.Inactive)
            {
                return (false, "Invalid period of activity: operatingUntil is required for an inactive railway");
            }

            return (true, "");
        }

        public override string ToString() =>
            $"{RailwayStatus} (since: {OperatingSince}, until: {OperatingUntil})";

        public override int GetHashCode() => HashCode.Combine(RailwayStatus, OperatingSince, OperatingUntil);

        public override bool Equals(object obj)
        {
            if (obj is PeriodOfActivity that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(PeriodOfActivity other) => AreEquals(this, other);

        public static bool operator ==(PeriodOfActivity left, PeriodOfActivity right) => AreEquals(left, right);

        public static bool operator !=(PeriodOfActivity left, PeriodOfActivity right) => !AreEquals(left, right);

        private static bool AreEquals(PeriodOfActivity left, PeriodOfActivity right) =>
            left.RailwayStatus == right.RailwayStatus &&
            left.OperatingSince == right.OperatingSince &&
            left.OperatingUntil == right.OperatingUntil;
    }
}