using System;

namespace TreniniDotNet.Domain.Collecting.ValueObjects
{
    public readonly struct CollectionItemId : IEquatable<CollectionItemId>
    {
        private readonly Guid _id;

        public static CollectionItemId NewId()
        {
            return new CollectionItemId(Guid.NewGuid());
        }

        public static CollectionItemId Empty => new CollectionItemId(Guid.Empty);

        public CollectionItemId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is CollectionItemId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(CollectionItemId left, CollectionItemId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(CollectionItemId left, CollectionItemId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(CollectionItemId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(CollectionItemId left, CollectionItemId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
