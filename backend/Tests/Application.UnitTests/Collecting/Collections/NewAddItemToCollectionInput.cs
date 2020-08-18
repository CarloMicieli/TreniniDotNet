using System;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Application.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public static class NewAddItemToCollectionInput
    {
        public static readonly AddItemToCollectionInput Empty = With();

        public static AddItemToCollectionInput With(
            Guid? id = null,
            string owner = null,
            string catalogItem = null,
            DateTime? addedDate = null,
            PriceInput price = null,
            string condition = null,
            string shop = null,
            string notes = null) =>
            new AddItemToCollectionInput(
                id ?? Guid.Empty,
                owner,
                catalogItem,
                addedDate ?? DateTime.MinValue,
                price ?? new PriceInput(0M, "EUR"),
                condition,
                shop,
                notes);
    }
}