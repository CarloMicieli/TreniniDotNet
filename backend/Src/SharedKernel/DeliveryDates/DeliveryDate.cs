using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TreniniDotNet.SharedKernel.DeliveryDates
{
    public readonly struct DeliveryDate : IEquatable<DeliveryDate>, IComparable<DeliveryDate>
    {
        private DeliveryDate(int year, Quarter? quarter)
        {
            Year = year;
            Quarter = quarter;
        }

        public static DeliveryDate FirstQuarterOf(int year) => new DeliveryDate(year, DeliveryDates.Quarter.Q1);
        public static DeliveryDate SecondQuarterOf(int year) => new DeliveryDate(year, DeliveryDates.Quarter.Q2);
        public static DeliveryDate ThirdQuarterOf(int year) => new DeliveryDate(year, DeliveryDates.Quarter.Q3);
        public static DeliveryDate FourthQuarterOf(int year) => new DeliveryDate(year, DeliveryDates.Quarter.Q4);

        public int Year { get; }
        public Quarter? Quarter { get; }

        public bool HasQuarter => Quarter.HasValue;

        public override string ToString() => this.HasQuarter ?
            $"{Year}/{Quarter}" : Year.ToString();

        public override int GetHashCode() => HashCode.Combine(Year, Quarter);

        #region [ Equality ]
        public override bool Equals(object? obj)
        {
            if (obj is DeliveryDate that)
            {
                return this.Equals(that);
            }

            return false;
        }

        public bool Equals(DeliveryDate that) =>
            this.Year == that.Year && this.Quarter == that.Quarter;

        public static bool operator ==(DeliveryDate left, DeliveryDate right) => left.Equals(right);

        public static bool operator !=(DeliveryDate left, DeliveryDate right) => !left.Equals(right);
        #endregion

        #region [ Comparable ]
        public int CompareTo(DeliveryDate that)
        {
            int compareYears = this.Year.CompareTo(that.Year);
            if (compareYears != 0)
            {
                return compareYears;
            }

            return QuarterToInt(this.Quarter).CompareTo(QuarterToInt(that.Quarter));
        }

        public static bool operator >(DeliveryDate left, DeliveryDate right) => left.CompareTo(right) > 0;

        public static bool operator >=(DeliveryDate left, DeliveryDate right) => left.CompareTo(right) >= 0;

        public static bool operator <(DeliveryDate left, DeliveryDate right) => left.CompareTo(right) < 0;

        public static bool operator <=(DeliveryDate left, DeliveryDate right) => left.CompareTo(right) <= 0;
        #endregion

        public static DeliveryDate? Parse(string? str)
        {
            if (TryParse(str, out var res))
            {
                return res;
            }

            throw new InvalidOperationException("Invalid value for a delivery date");
        }

        public static bool TryParse(string? str, [NotNullWhen(true)] out DeliveryDate? result)
        {
            if (str is null)
            {
                result = default;
                return false;
            }

            var dd = DeliveryDateParser.Parse(str);
            if (dd.HasValue)
            {
                result = dd.Value;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryCreate(int year, Quarter? quarter, out DeliveryDate result)
        {
            if (year > 1900 && year < 2999)
            {
                result = new DeliveryDate(year, quarter);
                return true;
            }

            result = default;
            return false;
        }

        private static int QuarterToInt(Quarter? qtr) => Quarters.QuarterToInt(qtr);

        private static class DeliveryDateParser
        {
            private static readonly Regex YearAndQuarterPattern = new Regex(@"(\d{4})/Q(\d)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            private static readonly Regex YearPattern = new Regex(@"(\d{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            internal static DeliveryDate? Parse(string str)
            {
                MatchCollection matchesYearAndQuarter = YearAndQuarterPattern.Matches(str);
                if (matchesYearAndQuarter.Count > 0)
                {
                    foreach (Match? match in matchesYearAndQuarter)
                    {
                        if (match != null)
                        {
                            GroupCollection groups = match.Groups;
                            string sYear = groups[1].Value;
                            string sQuarter = groups[2].Value;

                            return new DeliveryDate(int.Parse(sYear), Enum.Parse<Quarter>($"Q{sQuarter}"));
                        }
                    }
                }

                MatchCollection matchesJustYear = YearPattern.Matches(str);
                if (matchesJustYear.Count > 0)
                {
                    foreach (Match? match in matchesJustYear)
                    {
                        if (match != null)
                        {
                            GroupCollection groups = match.Groups;
                            string sYear = groups[1].Value;
                            return new DeliveryDate(int.Parse(sYear), null);
                        }
                    }
                }

                return null;
            }
        }
    }
}