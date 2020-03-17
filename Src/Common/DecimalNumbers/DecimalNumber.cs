using System;

namespace TreniniDotNet.Common.DecimalNumbers
{
    /// <summary>
    /// It represents a decimal number, internally represented as two
    /// integer val.
    /// </summary>
    public readonly struct DecimalNumber : IEquatable<DecimalNumber>, IComparable<DecimalNumber>
    {
        private readonly int? _value;
        private readonly int _factor;

        private DecimalNumber(int value, int factor)
        {
            if (factor <= 0)
            {
                throw new ArgumentException("factor must be positive");
            }

            if (factor != 1 && factor % 10 != 0)
            {
                throw new ArgumentException("factor must be a power of 10");
            }

            _value = value;
            _factor = factor;
        }

        /// <summary>
        /// Checks whether this <em>DecimalNumber</em> is positive (> 0).
        /// </summary>
        /// <value>
        /// <em>true</em> for positive values; <em>false</em> otherwise.
        /// </value>
        public bool IsPositive => _value > 0;
        public bool IsNegative => _value < 0;

        /// <summary>
        /// Checks whether this <em>DecimalNumber</em> contains a value.
        /// </summary>
        /// <value>
        /// <em>true</em> for positive values; <em>false</em> otherwise.
        /// </value>
        public bool IsNaN => _value.HasValue == false;

        public bool TryGetDouble(out double d)
        {
            if (_value.HasValue)
            {
                d = _value.Value / (double)_factor;
                return true;
            }
            else
            {
                d = double.NaN;
                return false;
            }
        }

        public bool TryGetFloat(out float f)
        {
            if (_value.HasValue)
            {
                f = _value.Value / (float)_factor;
                return true;
            }
            else
            {
                f = float.NaN;
                return false;
            }
        }

        public float ToFloat()
        {
            //TODO: #fixme
            bool _ = TryGetFloat(out float f);
            return f;
        }

        public override int GetHashCode() => _value.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is DecimalNumber that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(DecimalNumber left, DecimalNumber right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(DecimalNumber left, DecimalNumber right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(DecimalNumber other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(DecimalNumber @this, DecimalNumber that)
        {
            return @this._value == that._value && @this._factor == that._factor;
        }
        #endregion

        #region [ Comparators ]
        public int CompareTo(DecimalNumber other)
        {
            //TODO: #fixme
            return this.ToFloat().CompareTo(other.ToFloat());
        }

        public static bool operator >(DecimalNumber left, DecimalNumber right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(DecimalNumber left, DecimalNumber right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <(DecimalNumber left, DecimalNumber right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(DecimalNumber left, DecimalNumber right)
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion

        public override string ToString()
        {
            if (_value.HasValue)
            {
                return $"(value={_value}, factor={_factor})";
            }
            else
            {
                return "NaN";
            }
        }

        public static DecimalNumber NaN = new DecimalNumber();

        public static DecimalNumber Of(float value, Digits digits)
        {
            int factor = (int)digits;
            float n = value * factor;
            return new DecimalNumber((int)n, factor);
        }
    }

}
