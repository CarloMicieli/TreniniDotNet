using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class CollectionStatisticsView
    {
        private readonly ICollectionStats _inner;

        public CollectionStatisticsView(ICollectionStats stats)
        {
            _inner = stats;

            Details = stats.CategoriesByYear.Select(it => new CollectionStatItem(it))
                .OrderByDescending(it => it.Year)
                .ThenBy(it => it.Category)
                .ToList();
        }

        public string Owner => _inner.Owner.Value;

        public DateTime ModifiedDate => _inner.ModifiedDate.ToDateTimeUtc();

        public string TotalValue => _inner.TotalValue.ToString();

        public List<CollectionStatItem> Details { get; }

    }
}
