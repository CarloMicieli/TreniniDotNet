using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection
{
    public sealed class AddItemToCollectionInput : IUseCaseInput
    {
        public AddItemToCollectionInput(
            string collectionOwner,
            string catalogItem,
            DateTime addedDate,
            decimal price,
            string condition,
            string? shop,
            string? notes)
        {
            CollectionOwner = collectionOwner;
            CatalogItem = catalogItem;
            Shop = shop;
            Price = price;
            AddedDate = addedDate;
            Notes = notes;
            Condition = condition;
        }

        public string CollectionOwner { get; }
        public string CatalogItem { get; }
        public string? Shop { get; }
        public decimal Price { get; }
        public string Condition { get; }
        public DateTime AddedDate { get; }
        public string? Notes { get; }
    }
}
