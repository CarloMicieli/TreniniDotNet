using System;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemInput : IUseCaseInput
    {
        public EditWishlistItemInput(
            string owner,
            Guid id, Guid itemId,
            DateTime? addedDate,
            decimal? price,
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
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
