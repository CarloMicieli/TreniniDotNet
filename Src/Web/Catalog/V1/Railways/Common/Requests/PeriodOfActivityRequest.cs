using System;

namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.Requests
{
    public sealed class PeriodOfActivityRequest
    {
        public string? Status { get; set; }
        public DateTime? OperatingUntil { get; set; }
        public DateTime? OperatingSince { get; set; }
    }
}