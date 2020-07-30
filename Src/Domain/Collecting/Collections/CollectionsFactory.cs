using System;
using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionsFactory : AggregateRootFactory<CollectionId, Collection>
    {
        public CollectionsFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public Collection CreateCollection(Owner owner, string? notes)
        {
            return new Collection(
                NewId(id => new CollectionId(id)),
                owner,
                notes,
                new List<CollectionItem>(),
                GetCurrentInstant(),
                null,
                1
            );
        }
    }
}
