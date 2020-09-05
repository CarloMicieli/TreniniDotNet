using System;
using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Collecting.V1.Common.Responses;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections.Responses
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
