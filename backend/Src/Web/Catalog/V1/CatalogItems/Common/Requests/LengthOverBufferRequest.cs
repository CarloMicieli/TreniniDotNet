namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests
{
    public sealed class LengthOverBufferRequest
    {
        public decimal? Millimeters { get; set; }
        public decimal? Inches { get; set; }
    }
}