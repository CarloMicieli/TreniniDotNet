using System.Collections.Generic;
using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemRequest : IRequest
    {
        public string? Brand { set; get; }

        public string? ItemNumber { set; get; }

        public string? Description { set; get; }

        public string? PrototypeDescription { set; get; }

        public string? ModelDescription { set; get; }

        public string? PowerMethod { set; get; }

        public string? Scale { set; get; }

        public string? DeliveryDate { set; get; }

        public bool Available { set; get; } = false;

        public List<RollingStockRequest> RollingStocks { set; get; } = new List<RollingStockRequest>();
    }
}