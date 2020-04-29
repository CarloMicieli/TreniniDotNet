using System;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionInput : IUseCaseInput
    {
        public AddItemToCollectionInput(
            Guid id,
            string owner,
            string catalogItem,
            DateTime addedDate,
            decimal price,
            string condition,
            string? shop,
            string? notes)
        {
            Id = id;
            Owner = owner;
            CatalogItem = catalogItem;
            Shop = shop;
            Price = price;
            AddedDate = addedDate;
            Notes = notes;
            Condition = condition;
        }

        public Guid Id { get; }
        public string Owner { get; }
        public string CatalogItem { get; }
        public string? Shop { get; }
        public decimal Price { get; }
        public string Condition { get; }
        public DateTime AddedDate { get; }
        public string? Notes { get; }
    }
}
