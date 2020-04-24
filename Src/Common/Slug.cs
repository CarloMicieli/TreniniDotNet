using TreniniDotNet.Common.Extensions;
using System;

namespace TreniniDotNet.Common
{
    public readonly struct Slug : IEquatable<Slug>
    {
        private Slug(string slug)
        {
            Value = slug ??
                throw new ArgumentNullException(nameof(slug));
        }

        private static Slug NewSlug(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidSlugException("slug value cannot be blank or empty");
            }

            return new Slug(value.ToSeoFriendly());
        }

        public string Value { get; }

        private static readonly Slug EmptySlug = new Slug(string.Empty);

        public static implicit operator string(Slug slug) { return slug.Value; }

        public static Slug Of(string s)
        {
            return NewSlug(s);
        }

        public static Slug Of<T>(T value)
            where T : ICanConvertToSlug<T>
        {
            return value.ToSlug();
        }

        public static Slug Of(string value1, string value2)
        {
            return new Slug(value1.ToSeoFriendly() + "-" + value2.ToSeoFriendly());
        }

        public static Slug Of<T1, T2>(T1 value1, T2 value2)
            where T1 : ICanConvertToSlug<T1>
            where T2 : ICanConvertToSlug<T2>
        {
            return new Slug(value1.ToSlug().ToString() + "-" + value2.ToSlug().ToString());
        }

        public Slug CombineWith<T>(T value)
            where T : ICanConvertToSlug<T>
        {
            return new Slug(this.ToString() + "-" + value.ToSlug().ToString());
        }

        public static Slug Empty => EmptySlug;

        public override string ToString() => Value;

        public static bool operator ==(Slug left, Slug right) => AreEquals(left, right);

        public static bool operator !=(Slug left, Slug right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is Slug that)
            {
                return this.Value == that.Value;
            }

            return false;
        }

        public bool Equals(Slug other) => AreEquals(this, other);

        private static bool AreEquals(Slug left, Slug right)
        {
            return left.Value == right.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
