using System.Collections.Immutable;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public interface ICollection : IModifiableEntity
    {
        CollectionId CollectionId { get; }

        Owner Owner { get; }

        IImmutableList<ICollectionItem> Items { get; }
    }
}
