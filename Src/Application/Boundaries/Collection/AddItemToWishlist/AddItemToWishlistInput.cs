using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistInput : IUseCaseInput
    {
        public AddItemToWishlistInput(
            string owner,
            Guid id,
            string catalogItem,
            DateTime addedDate,
            decimal? price,
            string? priority,
            string? notes)
        {
            Owner = owner;
            Id = id;
            CatalogItem = catalogItem;
            AddedDate = addedDate;
            Price = price;
            Priority = priority;
            Notes = notes;
        }

        public string Owner { get; }
        public Guid Id { get; }
        public string CatalogItem { get; }
        public DateTime AddedDate { get; }
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
