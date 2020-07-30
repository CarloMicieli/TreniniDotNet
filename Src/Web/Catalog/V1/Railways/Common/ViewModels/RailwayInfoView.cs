using System;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels
{
    public sealed class RailwayInfoView
    {
        private readonly Railway _railway;

        public RailwayInfoView(Railway railway)
        {
            _railway = railway ??
                throw new ArgumentNullException(nameof(railway));
        }

        public Guid Id => _railway.Id;

        public string Name => _railway.Name;

        public string Slug => _railway.Slug.ToString();
    }
}
