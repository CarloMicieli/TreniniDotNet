using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public sealed class CreateRailwayInput : IUseCaseInput
    {
        public string? Name { get; }

        public string? CompanyName { get; }

        public string? Country { get; }

        public PeriodOfActivityInput PeriodOfActivity { get; }

        public TotalRailwayLengthInput TotalLength { get; }

        public RailwayGaugeInput Gauge { get; }

        public string? WebsiteUrl { get; }

        public string? Headquarters { get; }

        public CreateRailwayInput(
            string? name, string? companyName,
            string? country,
            PeriodOfActivityInput? periodOfActivity,
            TotalRailwayLengthInput? totalLength,
            RailwayGaugeInput? gauge,
            string? websiteUrl, string? headquarters)
        {
            Name = name;
            CompanyName = companyName;
            Country = country;
            PeriodOfActivity = periodOfActivity ?? PeriodOfActivityInput.Default();
            TotalLength = totalLength ?? TotalRailwayLengthInput.Default();
            Gauge = gauge ?? RailwayGaugeInput.Default();
            Headquarters = headquarters;
            WebsiteUrl = websiteUrl;
        }
    }
}
