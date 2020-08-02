using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class RailwayRef : AggregateRootRef<Railway, RailwayId>
    {
        public RailwayRef(RailwayId id, string slug, string name) 
            : base(id, slug, name)
        {
        }

        public RailwayRef(Railway railway)
            : this(railway.Id, railway.Slug, railway.Name)
        {
        }

        public static RailwayRef? AsOptional(Railway? railway) =>
            (railway is null) ? null : new RailwayRef(railway);
    }
}