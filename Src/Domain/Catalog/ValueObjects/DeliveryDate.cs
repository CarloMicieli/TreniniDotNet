using System;
using System.Text.RegularExpressions;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct DeliveryDate : IEquatable<DeliveryDate>, IComparable<DeliveryDate>
    {
        private readonly int _year;
        private readonly Quarter? _quarter;

        private DeliveryDate(int year, Quarter? quarter)
        {
            _year = year;
            _quarter = quarter;
        }

        public int Year => _year;
        public Quarter? Quarter => _quarter;

        public bool HasQuarter => _quarter.HasValue;

        public override string ToString() => this.HasQuarter ?
            $"{_year}/{_quarter}" : _year.ToString();

        public override int GetHashCode() => HashCode.Combine(_year, _quarter);

        #region [ Equality ]
        public override bool Equals(object obj)
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

        public static bool TryParse(string str, out DeliveryDate result)
        {
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
                    foreach (Match match in matchesYearAndQuarter)
                    {
                        GroupCollection groups = match.Groups;
                        string sYear = groups[1].Value;
                        string sQuarter = groups[2].Value;

                        return new DeliveryDate(int.Parse(sYear), Enum.Parse<Quarter>($"Q{sQuarter}"));
                    }
                }

                MatchCollection matchesJustYear = YearPattern.Matches(str);
                if (matchesJustYear.Count > 0)
                {
                    foreach (Match match in matchesJustYear)
                    {
                        GroupCollection groups = match.Groups;
                        string sYear = groups[1].Value;
                        return new DeliveryDate(int.Parse(sYear), null);
                    }
                }

                return null;
            }
        }
    }

    public enum Quarter
    {
        Q1,
        Q2,
        Q3,
        Q4
    }

    internal static class Quarters
    {
        internal static int QuarterToInt(Quarter? qtr) =>
            qtr switch
            {
                Quarter.Q1 => 1,
                Quarter.Q2 => 2,
                Quarter.Q3 => 3,
                Quarter.Q4 => 4,
                _ => Int32.MaxValue
            };
    }
}