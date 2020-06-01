namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests
{
    public sealed class RollingStockRequest
    {
        public string? Epoch { set; get; }

        public LengthOverBufferRequest? LengthOverBuffer { set; get; }

        public decimal? MinRadius { set; get; }

        public string? Railway { set; get; }

        public string? ClassName { set; get; }

        public string? RoadNumber { set; get; }

        public string? TypeName { set; get; }

        public string? Livery { set; get; }

        public string? Couplers { set; get; }

        public string? DccInterface { set; get; }

        public string? Control { set; get; }

        public string? Category { set; get; }

        public string? ServiceLevel { set; get; }

        public string? PassengerCarType { set; get; }
    }
}
