using System;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public readonly struct Owner
    {
        public string Value { get; }

        public Owner(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Input value for owner is empty or null", nameof(value));
            }

            this.Value = value;
        }

        public static implicit operator string(Owner o) { return o.Value; }

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            if (obj is Owner that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(Owner left, Owner right) => AreEquals(left, right);

        public static bool operator !=(Owner left, Owner right) => !AreEquals(left, right);

        private static bool AreEquals(Owner left, Owner right) =>
            left.Value == right.Value;

        #endregion

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => $"Owner({Value})";
    }
}
