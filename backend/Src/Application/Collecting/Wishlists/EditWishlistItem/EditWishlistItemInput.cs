using System;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemInput : IUseCaseInput
    {
        public EditWishlistItemInput(
            string owner,
            Guid id, Guid itemId,
            DateTime? addedDate,
            PriceInput? price,
            string? priority,
            string? notes)
        {
            Owner = owner;
            Id = id;
            ItemId = itemId;
            AddedDate = addedDate;
            Price = price;
            Priority = priority;
            Notes = notes;
        }

        public string Owner { get; }
        public Guid Id { get; }
        public Guid ItemId { get; }
        public DateTime? AddedDate { get; }
        public PriceInput? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
