using System;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public readonly struct CollectionItemId : IEquatable<CollectionItemId>
    {
        private Guid Value { get; }

        public CollectionItemId(Guid id)
        {
            Value = id;
        }

        public static CollectionItemId NewId()
        {
            return new CollectionItemId(Guid.NewGuid());
        }

        public static CollectionItemId Empty => new CollectionItemId(Guid.Empty);

        public static implicit operator Guid(CollectionItemId d) => d.Value;
        public static explicit operator CollectionItemId(Guid guid) => new CollectionItemId(guid);

        public Guid ToGuid() => Value;
        
        public override string ToString() => $"CollectionItemId({Value})";

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality ]

        public override bool Equals(object? obj)
        {
            if (obj is CollectionItemId that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(CollectionItemId left, CollectionItemId right) => left.Value == right.Value;

        public static bool operator !=(CollectionItemId left, CollectionItemId right) => !(left == right);

        public bool Equals(CollectionItemId other) => this == other;

        #endregion

        
    }
}
