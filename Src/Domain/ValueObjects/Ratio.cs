using System;
using TreniniDotNet.Common.Extensions;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    /// <summary>
    /// It represents the <em>Ratio</em> between a model railway size 
    /// and the size of an actual train.
    /// </summary>
    public readonly struct Ratio : IEquatable<Ratio>
    {
       // private const Digits DefaultDigits = Digits.One;

        private readonly decimal _ratio;

        private Ratio(decimal ratio)
        {
            if (!ratio.IsPositive())
            {
                throw new ArgumentException("ratio value must be positive");
            }
            _ratio = ratio;
        }

        public float ToFloat()
        {
            return (float) _ratio;
        }

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is Ratio that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Ratio left, Ratio right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Ratio left, Ratio right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(Ratio other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(Ratio left, Ratio right)
        {
            return left._ratio.Equals(right._ratio);
        }
        #endregion

        public override int GetHashCode()
        {
            return _ratio.GetHashCode();
        }

        public override string ToString()
        {
            return $"1:{_ratio}";
        }

        public static Ratio Of(decimal ratio)
        {
            return new Ratio(ratio);
        }

        public static Ratio Of(float ratio)
        {
            return new Ratio((decimal) ratio);
        }
    }
}
