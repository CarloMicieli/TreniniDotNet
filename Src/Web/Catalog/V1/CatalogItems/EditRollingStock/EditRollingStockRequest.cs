using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockRequest : IRequest
    {
        [JsonIgnore]
        public Slug? Slug { set; get; }

        [JsonIgnore]
        public RollingStockId? RollingStockId { set; get; }

        public string? Epoch { get; set; }

        public string? Category { get; set; }

        public string? Railway { get; set; }

        public string? ClassName { get; set; }

        public string? TypeName { get; set; }

        public string? RoadNumber { get; set; }

        public string? Series { get; set; }

        public string? Couplers { get; set; }

        public string? Livery { get; set; }

        public string? Depot { get; set; }

        public string? PassengerCarType { get; set; }

        public string? ServiceLevel { get; set; }

        public LengthOverBufferRequest? LengthOverBuffer { get; set; }

        public decimal? MinRadius { get; set; }

        public string? DccInterface { get; set; }

        public string? Control { get; set; }
    }
}
