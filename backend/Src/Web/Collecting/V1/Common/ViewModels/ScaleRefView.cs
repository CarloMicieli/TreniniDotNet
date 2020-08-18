using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class ScaleRefView
    {
        private readonly ScaleRef _scale;

        public ScaleRefView(ScaleRef scale)
        {
            _scale = scale;
        }

        public string Slug => _scale.Slug;
        public string Value => _scale.ToString();
    }
}
