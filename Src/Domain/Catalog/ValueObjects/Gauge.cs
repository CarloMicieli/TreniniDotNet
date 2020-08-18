using System;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.SharedKernel.Lengths;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    // Gauge is the distance between the inner edges of the two rails that it runs on.
    public readonly struct Gauge : IEquatable<Gauge>, IComparable<Gauge>
    {
        public decimal Value { get; }
        public MeasureUnit MeasureUnit { get; }

        private Gauge(float gauge, MeasureUnit mu)
            : this((decimal)gauge, mu)
        {
        }

        private Gauge(decimal gauge, MeasureUnit mu)
        {
            if (gauge.IsPositive())
            {
                Value = gauge;
                MeasureUnit = mu;
            }
            else
            {
                throw new ArgumentException("InvalidGaugeValue", nameof(gauge));
            }
        }

        public static Gauge Of(decimal value, MeasureUnit mu) => new Gauge(value, mu);

        public static Gauge OfMillimeters(float mm) => new Gauge(mm, MeasureUnit.Millimeters);

        public static Gauge OfMillimeters(decimal mm) => new Gauge(mm, MeasureUnit.Millimeters);

        public static Gauge OfInches(float inches) => new Gauge(inches, MeasureUnit.Inches);

        public static Gauge OfInches(decimal inches) => new Gauge(inches, MeasureUnit.Inches);

        public void Deconstruct(out decimal value, out MeasureUnit mu)
        {
            value = Value;
            mu = MeasureUnit;
        }

        #region [ Comparable ]
        public int CompareTo(Gauge other)
        {
            decimal otherValue = other.MeasureUnit
                .ConvertTo(this.MeasureUnit)
                .Convert(other.Value);
            return Value.CompareTo(otherValue);
        }

        public static bool operator >(Gauge left, Gauge right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Gauge left, Gauge right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <(Gauge left, Gauge right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Gauge left, Gauge right)
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion

        #region [ Equality ]
        public override bool Equals(object? obj)
        {
            if (obj is Gauge that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Gauge left, Gauge right) => AreEquals(left, right);

        public static bool operator !=(Gauge left, Gauge right) => !AreEquals(left, right);

        public bool Equals(Gauge other) => AreEquals(this, other);

        public bool Equals(Gauge other, bool silentlyConvert)
        {
            if (silentlyConvert)
            {
                decimal otherValue = other.MeasureUnit
                    .ConvertTo(this.MeasureUnit)
                    .Convert(other.Value);

                return this.Value.Equals(otherValue);
            }
            else
            {
                return AreEquals(this, other);
            }
        }

        private static bool AreEquals(Gauge left, Gauge right)
        {
            return left.Value.Equals(right.Value) &&
                left.MeasureUnit.Equals(right.MeasureUnit);
        }
        #endregion

        public override string ToString() => MeasureUnit.ToString(Value);

        public override int GetHashCode() => HashCode.Combine(Value, MeasureUnit);
    }
}
