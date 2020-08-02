using System;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels
{
    public sealed class RailwayInfoView
    {
        private readonly RailwayRef _railway;

        public RailwayInfoView(RailwayRef railway)
        {
            _railway = railway ??
                throw new ArgumentNullException(nameof(railway));
        }

        public Guid Id => _railway.Id;

        public string Name => _railway.ToString();

        public string Slug => _railway.Slug;
    }
}
