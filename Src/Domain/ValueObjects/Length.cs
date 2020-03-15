using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Common.Extensions;
using static TreniniDotNet.Domain.Catalog.ValueObjects.MeasureUnitsConvertions;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    /// <summary>
    /// It represents a length over buffers for a model railway
    /// </summary>
    public readonly struct Length : IEquatable<Length>, IComparable<Length>
    {
        private static readonly Length _zeroMms = new Length(0.0M, MeasureUnit.Millimeters);
        private static readonly Length _zeroInches = new Length(0.0M, MeasureUnit.Inches);

        private readonly decimal _value;
        private readonly MeasureUnit _measureUnit;

        private Length(decimal value, MeasureUnit measureUnit)
        {
            if (value.IsNegative())
            {
                throw new ArgumentException(ExceptionMessages.InvalidLengthNegativeValue);
            }

            _value = value;
            _measureUnit = measureUnit;
        }

        public void Deconstruct(out decimal value, out MeasureUnit measureUnit)
        {
            value = _value;
            measureUnit = _measureUnit;
        }

        public static Length ZeroMillimeters
        {
            get
            {
                return _zeroMms;
            }
        }

        public static Length ZeroInches
        {
            get
            {
                return _zeroInches;
            }
        }

        #region [ Equality ]
        public bool Equals(Length other)
        {
            return AreEquals(this, other);
        }

        public override bool Equals(object obj)
        {
            if (obj is Length that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Length left, Length right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Length left, Length right)
        {
            return !AreEquals(left, right);
        }

        private static bool AreEquals(Length left, Length right)
        {
            return left._measureUnit == right._measureUnit && 
                left._value == right._value;
        }
        #endregion

        #region [ Operations ]
        public static Length operator +(Length left, Length right)
        {
            var (lv, lmu) = left;
            var (rv, rmu) = right;

            return (lmu, rmu).Combine(lv, rv, (lv, rv, mu) => new Length(lv + rv, mu));
        }

        public Length Add(Length that)
        {
            return this + that;
        }
        #endregion

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
            var (lv, lmu) = this;
            var (rv, rmu) = other;

            return (lmu, rmu).Combine(lv, rv, (lv, rv, _) => lv.CompareTo(rv));
        }
        #endregion

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _measureUnit);
        }

        public override string ToString()
        {
      
            return _measureUnit.Apply(_value, val => $"{val.ToFloat()}in", val => $"{val.ToFloat()}mm");

        }

        public static Length OfMillimeters(float f)
        {
            return NewLength(f, MeasureUnit.Millimeters);
        }

        public static Length OfInches(float f)
        {
            return NewLength(f, MeasureUnit.Inches);
        }

        public static Length Of(decimal f)
        {
            return new Length(f, MeasureUnit.Millimeters);
        }

/*
        public static Length OfInches(decimal f)
        {
            return new Length(f, MeasureUnit.Inches);
        }
*/
        private static Length NewLength(float f, MeasureUnit mu)
        {
            return new Length((decimal) f, mu);
        }

        private (decimal, MeasureUnit) ExtractValues()
        {
            return (_value, _measureUnit);
        }

        public float ToMillimeters()
        {
            return MeasureUnit.Millimeters.GetValueOrConvert(ExtractValues).ToFloat();
        }

        public float ToInches()
        {
            return MeasureUnit.Inches.GetValueOrConvert(ExtractValues).ToFloat();
        }
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
            Length? first = lengths.FirstOrDefault();
            if (first.HasValue)
            {
                var (_, mu) = first.Value;

                var zero = mu.Apply(first.Value, (_) => Length.ZeroInches, (_) => Length.ZeroMillimeters);
                return lengths.Aggregate(zero, (acc, l) => acc + l);
            }

            return Length.ZeroMillimeters;
        }
    }
}
