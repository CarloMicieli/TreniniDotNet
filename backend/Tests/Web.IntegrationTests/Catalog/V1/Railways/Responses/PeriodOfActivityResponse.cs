using System;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses
{
    public class PeriodOfActivityResponse
    {
        public string Status { set; get; }
        public DateTime? OperatingSince { set; get; }
        public DateTime? OperatingUntil { set; get; }
    }
}