using System;
using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Collecting.V1.Common.Responses;
using TreniniDotNet.IntegrationTests.Collecting.V1.Shops.Responses;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections.Responses
{
    public class CollectionResponse
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public List<CollectionItemResponse> Items { get; set; }
    }

    public class CollectionItemResponse
    {
        public Guid ItemId { set; get; }

        public CatalogItemResponse CatalogItem { set; get; }

        public CatalogItemDetailsResponse Details { set; get; }

        public string Condition { set; get; }

        public MoneyResponse Price { set; get; }

        public ShopInfoResponse PurchasedAt { set; get; }

        public DateTime AddedDate { set; get; }

        public string Notes { set; get; }
    }
}
