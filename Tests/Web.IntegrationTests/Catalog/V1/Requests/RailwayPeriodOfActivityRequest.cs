using System;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Requests
{
    internal class RailwayPeriodOfActivityRequest
    {
        public string Status { set; get; }
        public DateTime? OperatingSince { set; get; }
        public DateTime? OperatingUntil { set; get; }
    }
}