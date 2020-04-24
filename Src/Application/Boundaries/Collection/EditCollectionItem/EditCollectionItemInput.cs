using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem
{
    public sealed class EditCollectionItemInput : IUseCaseInput
    {
        public EditCollectionItemInput(
            string owner,
            Guid id,
            Guid itemId,
            DateTime? addedDate,
            decimal? price,
            string? condition,
            string? shop,
            string? notes)
        {
            Owner = owner;
            Id = id;
            ItemId = itemId;
            Shop = shop;
            Price = price;
            AddedDate = addedDate;
            Notes = notes;
            Condition = condition;
        }

        public string Owner { get; }
        public Guid Id { get; }
        public Guid ItemId { get; }
        public string? Shop { get; }
        public decimal? Price { get; }
        public string? Condition { get; }
        public DateTime? AddedDate { get; }
        public string? Notes { get; }
    }
}
