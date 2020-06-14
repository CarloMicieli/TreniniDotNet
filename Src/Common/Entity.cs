using System;

namespace TreniniDotNet.Common
{
    public abstract class Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public static bool operator ==(Entity<TKey>? a, Entity<TKey>? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TKey>? a, Entity<TKey>? b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity<TKey> that)
            {
                if (ReferenceEquals(that, null))
                    return false;

                if (ReferenceEquals(this, that))
                    return true;

                if (GetType() != that.GetType())
                    return false;

                return Id.Equals(that.Id);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() =>
            HashCode.Combine(GetType().Name, Id);
    }
}
