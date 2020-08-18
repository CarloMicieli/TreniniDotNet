using System;
using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public static class NewRemoveItemFromCollectionInput
    {
        public static readonly RemoveItemFromCollectionInput Empty = With();

        public static RemoveItemFromCollectionInput With(
            string owner = null,
            Guid? id = null,
            Guid? itemId = null,
            DateTime? removed = null,
            string notes = null) => new RemoveItemFromCollectionInput(
            owner,
            id ?? Guid.Empty,
            itemId ?? Guid.Empty,
            removed);
    }
}