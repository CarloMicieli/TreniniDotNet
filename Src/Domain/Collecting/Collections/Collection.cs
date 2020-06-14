using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class Collection : AggregateRoot<CollectionId>, ICollection
    {
        internal Collection(
            CollectionId collectionId,
            Owner owner,
            IImmutableList<ICollectionItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
            : base(collectionId, createdDate, modifiedDate, version)
        {
            Owner = owner;
            Items = items;
        }

        public Owner Owner { get; }

        public IImmutableList<ICollectionItem> Items { get; }
    }
}
