using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
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
