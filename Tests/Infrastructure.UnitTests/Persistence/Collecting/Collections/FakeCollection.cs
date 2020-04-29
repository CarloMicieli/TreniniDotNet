using System;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
{
    internal class FakeCollection : ICollection
    {
        public CollectionId CollectionId =>
            new CollectionId(new Guid("5dcb04fa-6aab-4e92-baff-d480b099ee5e"));

        public Owner Owner => new Owner("George");

        public IImmutableList<ICollectionItem> Items => ImmutableList<ICollectionItem>.Empty;

        public Instant CreatedDate => Instant.FromUtc(2019, 11, 25, 9, 0);

        public Instant? ModifiedDate => null;

        public int Version => 1;
    }
}
