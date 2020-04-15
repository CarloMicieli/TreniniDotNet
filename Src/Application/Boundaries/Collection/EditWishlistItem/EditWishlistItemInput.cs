using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem
{
    public sealed class EditWishlistItemInput : IUseCaseInput
    {
        public EditWishlistItemInput(
            Guid id, Guid itemId,
            decimal? price,
            string? priority,
            string? notes)
        {
            Id = id;
            ItemId = itemId;
            Price = price;
            Priority = priority;
            Notes = notes;
        }

        public Guid Id { get; }
        public Guid ItemId { get; }
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
