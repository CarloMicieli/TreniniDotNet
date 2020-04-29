using System;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
