using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TreniniDotNet.Common.Extensions;

namespace TreniniDotNet.Common.Lengths
{
    // It represents a non negative unit of length.
    public readonly struct Length : IEquatable<Length>, IComparable<Length>
    {
        public decimal Value { get; }
        public MeasureUnit MeasureUnit { get; }

        public static Length ZeroMillimeters => new Length(decimal.Zero, MeasureUnit.Millimeters);
        public static Length ZeroInches => new Length(decimal.Zero, MeasureUnit.Inches);

        private Length(decimal value, MeasureUnit measureUnit)
        {
            if (value.IsNegative())
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Value = value;
            MeasureUnit = measureUnit;
        }

        public static Length Of(decimal value, MeasureUnit mu)
        {
            return new Length(value, mu);
        }

        public static Length OfMillimeters(decimal value)
        {
            return new Length(value, MeasureUnit.Millimeters);
        }

        public static Length OfInches(decimal value)
        {
            return new Length(value, MeasureUnit.Inches);
        }

        public static bool TryCreate(decimal? value, MeasureUnit measureUnit, [NotNullWhen(true)] out Length? result)
        {
            if (value.HasValue == false)
            {
                result = default;
                return false;
            }
            else
            {
                decimal d = value ?? 0M; // Just to make the compiler happy
                if (d.IsNegative())
                {
                    result = default;
                    return false;
                }
                else
                {
                    result = new Length(d, measureUnit);
                    return true;
                }
            }
        }

        public void Deconstruct(out decimal value, out MeasureUnit measureUnit)
        {
            value = Value;
            measureUnit = MeasureUnit;
        }

        #region [ Comparable ]
        public static bool operator >(Length left, Length right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Length left, Length right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <(Length left, Length right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Length left, Length right)
        {
            return left.CompareTo(right) <= 0;
        }

        public int CompareTo(Length other)
        {
            decimal otherValue = other.MeasureUnit
                .ConvertTo(this.MeasureUnit)
                .Convert(other.Value);
            return Value.CompareTo(otherValue);
        }
        #endregion

        #region [ Equality ]
        public static bool operator ==(Length left, Length right) => AreEquals(left, right);

        public static bool operator !=(Length left, Length right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is Length that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Length other) =>
            AreEquals(this, other);

        private static bool AreEquals(Length left, Length right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            decimal rightValue = right.MeasureUnit
                .ConvertTo(left.MeasureUnit)
                .Convert(right.Value);
            return left.Value.Equals(rightValue);
        }
        #endregion

        #region [ Operations ]
        public static Length operator +(Length left, Length right) => left.Add(right);

        public Length Add(Length that)
        {
            var value = this.Value;
            return (that.Value, that.MeasureUnit).As(this.MeasureUnit)
                .Apply((v, mu) => new Length(v + value, mu));
        }
        #endregion

        public override int GetHashCode() => HashCode.Combine(Value, MeasureUnit);

        public override string ToString() => MeasureUnit.ToString(Value);
    }

    public static class LengthExtensions
    {
        /// <summary>
        /// Sum a list of length values
        /// </summary>
        /// <remarks>
        /// In case the elements have different measure unit, all items are converted implicitly to the 
        /// first element measure unit and summed.
        /// 
        /// In case there are no elements, the result will default to a length of 0 millimeters.
        /// </remarks>
        /// <param name="lengths"></param>
        /// <returns></returns>
        public static Length Sum(this IEnumerable<Length> lengths)
        {
            Length? zero = lengths.FirstOrDefault();
            if (zero.HasValue)
            {
                return lengths.Skip(1).Aggregate(zero.Value, (acc, l) => acc + l);
            }

            return Length.ZeroMillimeters;
        }
    }
}