using System;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public sealed class RailwayInfoView
    {
        private readonly IRailwayInfo _railway;

        public RailwayInfoView(IRailwayInfo railway)
        {
            _railway = railway ??
                throw new ArgumentNullException(nameof(railway));
        }

        public Guid Id => _railway.RailwayId.ToGuid();

        public string Name => _railway.Name;

        public string Slug => _railway.Slug.ToString();
    }
}
