using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionView
    {
        private readonly Collection _inner;

        public CollectionView(Collection collection)
        {
            _inner = collection;
            Items = collection.Items.Select(it => new CollectionItemView(it)).ToList();
        }

        public Guid Id => _inner.Id;
        public string Owner => _inner.Owner.Value;
        public IEnumerable<CollectionItemView> Items { get; }
    }
}