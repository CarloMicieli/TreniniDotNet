using System;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public readonly struct CollectionId : IEquatable<CollectionId>, IComparable<CollectionId>
    {
        private Guid Value { get; }

        public CollectionId(Guid id)
        {
            Value = id;
        }

        public static CollectionId NewId()
        {
            return new CollectionId(Guid.NewGuid());
        }

        public static CollectionId Empty => new CollectionId(Guid.Empty);

        public static implicit operator Guid(CollectionId d) => d.Value;
        public static explicit operator CollectionId(Guid guid) => new CollectionId(guid);

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality ]

        public override bool Equals(object? obj)
        {
            if (obj is CollectionId that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(CollectionId left, CollectionId right) => left.Value == right.Value;

        public static bool operator !=(CollectionId left, CollectionId right) => !(left == right);

        public bool Equals(CollectionId other) => this == other;

        #endregion

        public int CompareTo(CollectionId other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
