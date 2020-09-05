using System;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistInput : IUseCaseInput
    {
        public AddItemToWishlistInput(
            string owner,
            Guid id,
            string catalogItem,
            DateTime addedDate,
            PriceInput? price,
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
        public PriceInput? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
