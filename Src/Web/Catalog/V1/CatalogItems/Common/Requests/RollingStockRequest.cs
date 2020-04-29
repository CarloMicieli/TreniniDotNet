namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests
{
    public sealed class RollingStockRequest
    {
        public string? Era { set; get; }

        public LengthOverBufferRequest? Length { set; get; }

        public string? Railway { set; get; }

        public string? ClassName { set; get; }

        public string? RoadNumber { set; get; }

        public string? TypeName { get; }

        public string? DccInterface { get; }

        public string? Control { get; }

        public string? Category { set; get; }
    }
}