namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class AddressInput
    {
        public string? Line1 { set; get; }
        public string? Line2 { set; get; }
        public string? City { set; get; }
        public string? Region { set; get; }
        public string? PostalCode { set; get; }
        public string? Country { set; get; }
    }
}