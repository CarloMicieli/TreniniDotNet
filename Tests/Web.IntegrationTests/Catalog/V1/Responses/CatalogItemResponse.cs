using System;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Responses
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
