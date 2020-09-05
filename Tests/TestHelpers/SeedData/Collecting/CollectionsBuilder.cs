using System;
using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class CollectionsBuilder
    {
        private CollectionId _collectionId;
        private Owner _owner;
        private string _notes;
        private List<CollectionItem> _items;
        private Instant _createdDate;
        private Instant? _modifiedDate;
        private int _version;

        internal CollectionsBuilder()
        {
            _collectionId = CollectionId.NewId();
            _createdDate = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _items = new List<CollectionItem>();
            _modifiedDate = null;
            _version = 1;
        }

        public CollectionsBuilder Id(Guid id)
        {
            _collectionId = new CollectionId(id);
            return this;
        }

        public CollectionsBuilder Owner(Owner owner)
        {
            _owner = owner;
            return this;
        }

        public CollectionsBuilder Notes(string notes)
        {
            _notes = notes;
            return this;
        }

        public CollectionsBuilder CreatedDate(Instant createdDate)
        {
            _createdDate = createdDate;
            return this;
        }

        public CollectionsBuilder Item(Action<CollectionItemsBuilder> actionBuilder)
        {
            var itemBuilder = new CollectionItemsBuilder();
            actionBuilder(itemBuilder);
            _items.Add(itemBuilder.Build());
            return this;
        }

        public Collection Build()
        {
            return new Collection(
                _collectionId,
                _owner,
                _notes,
                _items,
                _createdDate,
                _modifiedDate,
                _version);
        }
    }
}
