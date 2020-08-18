using System;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionInput : IUseCaseInput
    {
        public AddItemToCollectionInput(
            Guid id,
            string owner,
            string catalogItem,
            DateTime addedDate,
            PriceInput price,
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
        public PriceInput Price { get; }
        public string Condition { get; }
        public DateTime AddedDate { get; }
        public string? Notes { get; }
    }
}
