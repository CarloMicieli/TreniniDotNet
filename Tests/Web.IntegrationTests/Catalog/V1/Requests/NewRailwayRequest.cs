namespace TreniniDotNet.IntegrationTests.Catalog.V1.Requests
{
    internal class NewRailwayRequest
    {
        public string Name { get; set; } = null!;
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public RailwayPeriodOfActivityRequest PeriodOfActivity { set; get; }
    }
}