using System;

namespace TreniniDotNet.Common.Domain
{
    public abstract class AggregateRootRef<TAggregate, TKey>
        where TKey: struct, IEquatable<TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        protected AggregateRootRef(TKey id, string slug, string stringRepresentation)
        {
            Id = id;
            Slug = slug;
            StringRepresentation = stringRepresentation;
        }

        public TKey Id { get; }
        public string Slug { get; }
        private string StringRepresentation { get; }

        public override string ToString() => StringRepresentation;

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is AggregateRootRef<TAggregate, TKey> that)
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
        
        public static bool operator ==(AggregateRootRef<TAggregate, TKey> a, AggregateRootRef<TAggregate, TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(AggregateRootRef<TAggregate, TKey> a, AggregateRootRef<TAggregate, TKey> b)
        {
            return !(a == b);
        }
    }
}