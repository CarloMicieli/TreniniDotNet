using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollection
    {
        CollectionId Id { get; }

        Owner Owner { get; }

        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }

        IImmutableList<ICollectionItem> Items { get; }
    }
}
