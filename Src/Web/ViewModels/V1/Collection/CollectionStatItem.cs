using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
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
