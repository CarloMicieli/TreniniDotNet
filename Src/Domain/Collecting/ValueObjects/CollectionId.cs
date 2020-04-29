using System;

namespace TreniniDotNet.Domain.Collecting.ValueObjects
{
    public readonly struct CollectionId : IEquatable<CollectionId>
    {
        private readonly Guid _id;

        public static CollectionId NewId()
        {
            return new CollectionId(Guid.NewGuid());
        }

        public static CollectionId Empty => new CollectionId(Guid.Empty);

        public CollectionId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is CollectionId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(CollectionId left, CollectionId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(CollectionId left, CollectionId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(CollectionId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(CollectionId left, CollectionId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
