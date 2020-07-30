using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionStatItem
    {
        private readonly CollectionStatsItem _item;

        public CollectionStatItem(CollectionStatsItem item)
        {
            _item = item;
        }

        public string Category => _item.Category.ToString();

        public int Count => _item.Count;

        public int Year => _item.Year.Value;

        public PriceView TotalValue => new PriceView(_item.TotalValue);
    }
}
