using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionInput : IUseCaseInput
    {
        public RemoveItemFromCollectionInput(
            Guid id, Guid itemId,
            DateTime? removed,
            string? notes)
        {
            Id = id;
            ItemId = itemId;
            Removed = removed;
            Notes = notes;
        }

        public Guid Id { get; }
        public Guid ItemId { get; }
        public DateTime? Removed { get; }
        public string? Notes { get; }
    }
}
