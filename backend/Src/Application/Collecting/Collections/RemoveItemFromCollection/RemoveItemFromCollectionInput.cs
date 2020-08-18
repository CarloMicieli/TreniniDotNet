using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionInput : IUseCaseInput
    {
        public RemoveItemFromCollectionInput(
            string owner,
            Guid id, Guid itemId,
            DateTime? removed)
        {
            Owner = owner;
            Id = id;
            ItemId = itemId;
            Removed = removed;
        }

        public string Owner { get; }
        public Guid Id { get; }
        public Guid ItemId { get; }
        public DateTime? Removed { get; }
    }
}
