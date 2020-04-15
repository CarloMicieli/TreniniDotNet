using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistInput : IUseCaseInput
    {
        public AddItemToWishlistInput(Guid id,
            string brand, string itemNumber,
            decimal? price,
            string? priority,
            string? notes)
        {
            Id = id;
            Brand = brand;
            ItemNumber = itemNumber;
            Price = price;
            Priority = priority;
            Notes = notes;
        }

        public Guid Id { get; }
        public string Brand { get; }
        public string ItemNumber { get; }
        public decimal? Price { get; }
        public string? Priority { get; }
        public string? Notes { get; }
    }
}
