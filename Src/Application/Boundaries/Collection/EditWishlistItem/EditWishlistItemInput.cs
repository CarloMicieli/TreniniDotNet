using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem
{
    public sealed class EditWishlistItemInput : IUseCaseInput
    {
        public EditWishlistItemInput(
            Guid id, Guid itemId,
            DateTime? addedDate,
            decimal? price,
            string? priority,
            string? notes)
        {
            Id = id;
            ItemId = itemId;
            AddedDate = addedDate;
            Price = price;
            Priority = priority;
            Notes = notes;
        }

        public Guid Id { get; }
        public Guid ItemId { get; }
        public DateTime? AddedDate { get; }
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
