using System;
using TreniniDotNet.IntegrationTests.Catalog.V1.Brands.Responses;
using TreniniDotNet.IntegrationTests.Catalog.V1.Scales.Responses;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses
{
    public class CatalogItemResponse
    {
        public SelfLinks _Links { set; get; }
        public Guid Id { set; get; }
        public string ItemNumber { set; get; }
        public BrandInfoResponse Brand { get; }
        public string Description { set; get; }
        public string PrototypeDescription { set; get; }
        public string ModelDescription { set; get; }
        public string DeliveryDate { set; get; }
        public bool Available { set; get; }
        public ScaleInfoResponse Scale { get; }
        public string PowerMethod { set; get; }
    }
}
