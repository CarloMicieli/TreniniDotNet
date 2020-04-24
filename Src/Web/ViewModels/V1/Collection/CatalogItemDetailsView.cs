using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class CatalogItemDetailsView
    {
        private ICatalogItemDetails _details;

        public CatalogItemDetailsView(ICatalogItemDetails details)
        {
            _details = details;
        }

        public BrandRefView Brand => new BrandRefView(_details.Brand);
        public string ItemNumber => _details.ItemNumber.Value;
        public string Category => _details.Category.ToString();
        public ScaleRefView Scale => new ScaleRefView(_details.Scale);
        public int Count => _details.RollingStocksCount;
        public string Description => _details.Description;
    }
}
