using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection
{
    public sealed class AddItemToCollectionInput : IUseCaseInput
    {
        public AddItemToCollectionInput(Guid id,
            string brand, string itemNumber,
            DateTime addedDate,
            decimal price,
            string condition,
            string? shop,
            string? notes)
        {
            Id = id;
            Brand = brand;
            ItemNumber = itemNumber;
            Shop = shop;
            Price = price;
            AddedDate = addedDate;
            Notes = notes;
            Condition = condition;
        }

        public Guid Id { get; }
        public string Brand { get; }
        public string ItemNumber { get; }
        public string? Shop { get; }
        public decimal Price { get; }
        public string Condition { get; }
        public DateTime AddedDate { get; }
        public string? Notes { get; }
    }
}
