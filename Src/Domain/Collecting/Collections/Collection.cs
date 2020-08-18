using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class Collection : AggregateRoot<CollectionId>
    {
        public Collection(
            CollectionId collectionId,
            Owner owner,
            string? notes,
            IEnumerable<CollectionItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
            : base(collectionId, createdDate, modifiedDate, version)
        {
            Owner = owner;
            Notes = notes;
            _items = items.ToList();
        }

        public Owner Owner { get; }
        public string? Notes { get; }

        private readonly List<CollectionItem> _items;
        public IReadOnlyCollection<CollectionItem> Items => _items.ToImmutableList();

        public void AddItem(CollectionItem collectionItem)
        {
            _items.Add(collectionItem);
        }

        public void UpdateItem(CollectionItem modifiedItem)
        {
            _items.RemoveAll(it => it.Id == modifiedItem.Id);
            _items.Add(modifiedItem);
        }

        public void RemoveItem(CollectionItemId itemId)
        {
            _items.RemoveAll(it => it.Id == itemId);
        }

        public CollectionItem? FindItemById(CollectionItemId itemId) =>
            _items.FirstOrDefault(it => it.Id == itemId);

        public Collection With(string? notes = null)
        {
            return new Collection(Id, Owner, notes ?? Notes, _items, CreatedDate, ModifiedDate, Version);
        }
    }
}
