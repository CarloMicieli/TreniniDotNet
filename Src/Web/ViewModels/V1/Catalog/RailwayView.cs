using System;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public sealed class RailwayView
    {
        public Guid RailwayId { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? Country { get; set; }

        public string? Status { get; set; }

        public DateTime? OperatingUntil { get; set; }

        public DateTime? OperatingSince { get; set; }

        public RailwayView(IRailway railway)
        {
            this.RailwayId = railway.RailwayId.ToGuid();
            this.Slug = railway.Slug.ToString();
            this.Name = railway.Name;
            this.CompanyName = railway.CompanyName;
            this.Country = railway.Country;
            this.Status = railway.Status.ToString();
            this.OperatingSince = railway.OperatingSince;
            this.OperatingUntil = railway.OperatingUntil;
        }
    }
}
