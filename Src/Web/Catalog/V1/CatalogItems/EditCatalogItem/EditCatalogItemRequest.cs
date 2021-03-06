﻿using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemRequest : IRequest
    {
        [JsonIgnore]
        public Slug? Slug { set; get; }

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
