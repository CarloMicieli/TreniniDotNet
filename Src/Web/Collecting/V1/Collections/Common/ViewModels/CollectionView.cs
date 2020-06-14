using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionView
    {
        private readonly ICollection _inner;

        public CollectionView(ICollection collection)
        {
            _inner = collection;
            Items = collection.Items.Select(it => new CollectionItemView(it)).ToList();
        }

        public Guid Id => _inner.Id.ToGuid();
        public string Owner => _inner.Owner.Value;
        public IEnumerable<CollectionItemView> Items { get; }
    }
}