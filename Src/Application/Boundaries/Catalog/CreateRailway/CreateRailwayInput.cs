using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public sealed class CreateRailwayInput : IUseCaseInput
    {
        private readonly string? _name;

        private readonly string? _companyName;

        private readonly string? _country;

        private readonly string? _status;

        private readonly DateTime? _operatingUntil;

        private readonly DateTime? _operatingSince;

        public CreateRailwayInput(string? name, string? companyName, string? country, string? status, DateTime? operatingSince, DateTime? operatingUntil)
        {
            _name = name;
            _companyName = companyName;
            _country = country;
            _status = status;
            _operatingUntil = operatingUntil;
            _operatingSince = operatingSince;
        }

        public string? Name => _name;

        public string? CompanyName => _companyName;

        public string? Country => _country;

        public string? Status => _status;

        public DateTime? OperatingUntil => _operatingUntil;

        public DateTime? OperatingSince => _operatingSince;
    }
}
