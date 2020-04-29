using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CollectionStatItem
    {
        private readonly ICollectionStatsItem _item;

        public CollectionStatItem(ICollectionStatsItem item)
        {
            _item = item;
        }

        public string Category => _item.Category.ToString();

        public int Count => _item.Count;

        public int Year => _item.Year.Value;

        public MoneyView TotalValue => new MoneyView(_item.TotalValue);
    }
}
