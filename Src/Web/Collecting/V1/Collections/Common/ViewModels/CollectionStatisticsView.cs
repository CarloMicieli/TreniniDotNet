using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionStatisticsView
    {
        private readonly CollectionStats _inner;

        public CollectionStatisticsView(CollectionStats stats)
        {
            _inner = stats;

            Details = stats.CategoriesByYear.Select(it => new CollectionStatItem(it))
                .OrderByDescending(it => it.Year)
                .ThenBy(it => it.Category)
                .ToList();
        }

        public Guid Id => _inner.Id;

        public string Owner => _inner.Owner.Value;

        public DateTime ModifiedDate => _inner.ModifiedDate.ToDateTimeUtc();

        public PriceView TotalValue => new PriceView(_inner.TotalValue);

        public List<CollectionStatItem> Details { get; }

    }
}
