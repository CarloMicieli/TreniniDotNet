using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway
{
    public sealed class CreateRailwayRequest : IRequest
    {
        public string Name { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? Country { get; set; }

        public string? Status { get; set; }

        public DateTime? OperatingUntil { get; set; }

        public DateTime? OperatingSince { get; set; }
    }
}
