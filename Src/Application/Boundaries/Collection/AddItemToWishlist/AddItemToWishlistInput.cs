using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistInput : IUseCaseInput
    {
        public AddItemToWishlistInput(
            Guid id,
            string catalogItem,
            DateTime addedDate,
            decimal? price,
            string? priority,
            string? notes)
        {
            Id = id;
            CatalogItem = catalogItem;
            AddedDate = addedDate;
            Price = price;
            Priority = priority;
            Notes = notes;
        }

        public Guid Id { get; }
        public string CatalogItem { get; }
        public DateTime AddedDate { get; }
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
