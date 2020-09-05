using System;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Application.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public static class NewEditCollectionItemInput
    {
        public static readonly EditCollectionItemInput Empty = With();

        public static EditCollectionItemInput With(
            string owner = null,
            Guid? id = null,
            Guid? itemId = null,
            DateTime? addedDate = null,
            PriceInput price = null,
            string condition = null,
            string shop = null,
            string notes = null) =>
            new EditCollectionItemInput(
                owner,
                id ?? Guid.Empty,
                itemId ?? Guid.Empty,
                addedDate ?? DateTime.MinValue,
                price,
                condition,
                shop,
                notes);
    }
}
