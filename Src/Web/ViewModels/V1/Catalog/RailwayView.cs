using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public sealed class RailwayView
    {
        public RailwayView(IRailway railway, LinksView? selfLink)
        {
            this.Links = selfLink;
            this.Id = railway.RailwayId.ToGuid();
            this.Name = railway.Name;
            this.CompanyName = railway.CompanyName;
            this.Country = railway.Country;
            this.Status = railway.Status.ToString();
            this.OperatingSince = railway.OperatingSince;
            this.OperatingUntil = railway.OperatingUntil;
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { set; get; }

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? Country { get; set; }

        public string? Status { get; set; }

        public DateTime? OperatingUntil { get; set; }

        public DateTime? OperatingSince { get; set; }
    }
}
