using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    /// <summary>
    /// A model trains Gauge is the distance between the inner edges of the two rails that it runs on.
    /// </summary>
    public readonly struct Gauge : IEquatable<Gauge>, IComparable<Gauge>
    {
        private const int DefaultDigits = 2;

        private readonly decimal _gauge;
        private readonly MeasureUnit _mu;

        private Gauge(decimal gauge, MeasureUnit mu)
        {
            if (gauge <= 0M)
            {
                throw new ArgumentException(ExceptionMessages.InvalidGaugeValue);
            }

            _gauge = gauge;
            _mu = mu;
        }

        /// <summary>
        /// Returns this <em>Gauge</em> value as Float, with the provided measure unit.
        /// </summary>
        /// <remarks>
        /// If the provided <em>MeasureUnit</em> is different from the one used to create this value
        /// the returned value is calculated using a conversion method.
        /// </remarks>
        /// <param name="mu"></param>
        /// <returns></returns>
        public float ToFloat(MeasureUnit mu)
        {
            var self = this;
            return (float)mu.GetValueOrConvert(() => (self._gauge, self._mu));
        }

        public decimal ToDecimal(MeasureUnit mu)
        {
            var self = this;
            return mu.GetValueOrConvert(() => (self._gauge, self._mu));
        }

        public void Deconstruct(out decimal v, out MeasureUnit mu)
        {
            v = _gauge;
            mu = _mu;
        }

        #region [ Comparable ]
        public int CompareTo(Gauge other)
        {
            var (lg, lmu) = this;
            var (rg, rmu) = other;
            return (lmu, rmu).Combine(lg, rg, (l, r, mu) => l.CompareTo(r));
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
        public override bool Equals(object obj)
        {
            if (obj is Gauge that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Gauge left, Gauge right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Gauge left, Gauge right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(Gauge other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(Gauge left, Gauge right)
        {
            return left._gauge.Equals(right._gauge);
        }
        #endregion

        public override string ToString()
        {
            return _mu.Apply<Gauge, string>(this,
                gauge => $"{gauge.ToFloat(MeasureUnit.Inches)}in",
                gauge => $"{gauge.ToFloat(MeasureUnit.Millimeters)}mm");
        }

        public override int GetHashCode()
        {
            return _gauge.GetHashCode();
        }

        /// <summary>
        /// It creates a new <em>Gauge</em> for the provided value expressed in millimeters.
        /// </summary>
        /// <param name="v">the value (in millimeters)</param>
        /// <returns>a new <em>Gauge</em></returns>
        public static Gauge OfMillimiters(float v)
        {
            return new Gauge((decimal)v, MeasureUnit.Millimeters);
        }

        public static Gauge OfMillimiters(decimal d)
        {
            return new Gauge(d, MeasureUnit.Millimeters);
        }

        /// <summary>
        /// It creates a new <em>Gauge</em> for the provided value expressed in inches.
        /// </summary>
        /// <param name="v">the value (in inches)</param>
        /// <returns>a new <em>Gauge</em></returns>
        public static Gauge OfInches(float v)
        {
            return new Gauge((decimal)v, MeasureUnit.Inches);
        }

        public static Gauge OfInches(decimal d)
        {
            return new Gauge(d, MeasureUnit.Inches);
        }
    }
}
