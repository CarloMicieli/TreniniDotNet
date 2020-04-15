using PhoneNumbers;
using System;

namespace TreniniDotNet.Common.PhoneNumbers
{
    public readonly struct PhoneNumber : IEquatable<PhoneNumber>
    {
        private static PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        public string Value { get; }

        private PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            var _ = phoneNumberUtil.Parse(value, null);
            Value = value;
        }

        public static bool IsValid(string value)
        {
            try
            {
                var _ = phoneNumberUtil.Parse(value, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static PhoneNumber Of(string phone) =>
            new PhoneNumber(phone);

        public override string ToString() => Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(PhoneNumber other) => AreEquals(this, other);

        private static bool AreEquals(PhoneNumber left, PhoneNumber right) =>
            left.Value == right.Value;

        public static bool operator ==(PhoneNumber left, PhoneNumber right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PhoneNumber left, PhoneNumber right)
        {
            return !(left == right);
        }
    }
}
