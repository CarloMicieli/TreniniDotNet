﻿using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionPresenter : DefaultHttpResultPresenter<RemoveItemFromCollectionOutput>, IRemoveItemFromCollectionOutputPort
    {
        public void CollectionItemNotFound(CollectionId collectionId, CollectionItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(RemoveItemFromCollectionOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
