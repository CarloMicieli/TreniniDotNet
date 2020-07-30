using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class CatalogItemDetailsView
    {
        private CatalogItem CatalogItem { get; }

        public CatalogItemDetailsView(CatalogItem catalogItem)
        {
            CatalogItem = catalogItem;
        }

        public BrandRefView Brand => new BrandRefView(CatalogItem.Brand);
        public string ItemNumber => CatalogItem.ItemNumber.Value;
        public string Category => CatalogItem.Category.ToString();
        public ScaleRefView Scale => new ScaleRefView(CatalogItem.Scale);
        public int Count => CatalogItem.Count;
        public string Description => CatalogItem.Description;
    }
}
