using System;
using System.Collections.Generic;

namespace TreniniDotNet.IntegrationTests.Collection.V1.Responses
{
    public class CollectionStatisticsResponse
    {
        public Guid Id { set; get; }

        public string Owner { set; get; }

        public DateTime ModifiedDate { set; get; }

        public MoneyResponse TotalValue { set; get; }

        public List<CollectionStatItemResponse> Details { set; get; }
    }

    public class CollectionStatItemResponse
    {
        public string Category { set; get; }

        public int Count { set; get; }

        public int Year { set; get; }

        public MoneyResponse TotalValue { set; get; }
    }
}
