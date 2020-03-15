using TreniniDotNet.Common.Extensions;
using System;

namespace TreniniDotNet.Common
{
    public readonly struct Slug
    {
        private readonly string _value;
        private static Slug EmptySlug = new Slug(string.Empty);

        private Slug(string slug)
        {
            _value = slug ??
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

        public string Value => _value;

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

        public override string ToString()
        {
            return _value;
        }

        public static bool operator ==(Slug left, Slug right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Slug left, Slug right)
        {
            return !AreEquals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (obj is Slug that)
            {
                return this.Value == that.Value;
            }

            return false;
        }

        /// <summary>
        /// It returns <em>this</em> value if not empty, otherwise it
        /// will produce a fresh value from the generation function.
        /// </summary>
        /// <param name="func">a <em>Slug</em> generation function</param>
        /// <returns>a <em>Slug</em></returns>
        public Slug OrNewIfEmpty(Func<Slug> func)
        {
            if (this == Slug.Empty)
            {
                return func.Invoke();
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Returns an empty <em>Slug</em>.
        /// </summary>
        public static Slug Empty
        {
            get
            {
                return EmptySlug;
            }
        }

        private static bool AreEquals(Slug left, Slug right)
        {
            return left.Value == right.Value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
