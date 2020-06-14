using System;
using NodaTime;

namespace TreniniDotNet.Common
{
    public readonly struct Year : IEquatable<Year>, IComparable<Year>
    {
        private Year(int value)
        {
            Value = value;
        }

        public static Year Of(int year)
        {
            return new Year(year);
        }

        public static Year FromLocalDate(LocalDate date)
        {
            return new Year(date.Year);
        }

        public int Value { get; }

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            if (obj is Year that)
            {
                return this.Value == that.Value;
            }

            return false;
        }

        public int CompareTo(Year other) =>
            this.Value.CompareTo(other.Value);

        public bool Equals(Year other) =>
            this.Value.Equals(other.Value);

        public static bool operator ==(Year left, Year right) => left.Equals(right);

        public static bool operator !=(Year left, Year right) => !(left == right);

        public static bool operator >(Year left, Year right) => left.Value > right.Value;

        public static bool operator <(Year left, Year right) => !(left > right);

        public static bool operator >=(Year left, Year right) => left.Value >= right.Value;

        public static bool operator <=(Year left, Year right) => !(left > right);

        #endregion

        public override int GetHashCode() => this.Value.GetHashCode();
    }
}
