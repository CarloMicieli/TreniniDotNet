using System;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class Collection : ICollection, IEquatable<Collection>
    {
        internal Collection(CollectionId collectionId,
            Owner owner,
            IImmutableList<ICollectionItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
        {
            CollectionId = collectionId;
            Owner = owner;
            Items = items;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Version = version;
        }

        public CollectionId CollectionId { get; }

        public Owner Owner { get; }

        public IImmutableList<ICollectionItem> Items { get; }

        public Instant CreatedDate { get; }

        public Instant? ModifiedDate { get; }

        public int Version { get; }

        #region [ Equality ]
        public static bool operator ==(Collection left, Collection right) => AreEquals(left, right);

        public static bool operator !=(Collection left, Collection right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is Collection that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Collection other) => AreEquals(this, other);

        private static bool AreEquals(Collection left, Collection right) =>
            left.CollectionId == right.CollectionId;

        public override int GetHashCode() => CollectionId.GetHashCode();
        #endregion
    }
}
