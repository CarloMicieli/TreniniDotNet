using System;
using System.Collections.Generic;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Responses
{
    public class WishlistResponse
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public string ListName { get; set; }

        public string Visibility { get; set; }

        public string Owner { get; set; }

        public List<WishlistItemResponse> Items { get; }
    }

    public class WishlistItemResponse
    {
        public Guid ItemId { get; set; }

        public string Priority { get; set; }

        public DateTime AddedDate { get; set; }

        public MoneyResponse Price { get; set; }

        public CatalogItemResponse CatalogItem { get; set; }

        public CatalogItemDetailsResponse Details { get; set; }

        public string Notes { get; set; }
    }
}
