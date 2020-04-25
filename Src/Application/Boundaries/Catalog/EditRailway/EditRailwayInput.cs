using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditRailway
{
    public sealed class EditRailwayInput : IUseCaseInput
    {
        public Slug RailwaySlug { get; }
        public ModifiedRailwayValues Values { get; }

        public EditRailwayInput(
            Slug slug,
            string? name, string? companyName,
            string? country,
            PeriodOfActivityInput? periodOfActivity,
            TotalRailwayLengthInput? totalLength,
            RailwayGaugeInput? gauge,
            string? websiteUrl, string? headquarters)
        {
            RailwaySlug = slug;
            Values = new ModifiedRailwayValues(
                name, companyName,
                country,
                periodOfActivity,
                totalLength,
                gauge,
                websiteUrl, headquarters);
        }
    }

    public sealed class ModifiedRailwayValues
    {
        public ModifiedRailwayValues(
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

        public string? Name { get; }

        public string? CompanyName { get; }

        public string? Country { get; }

        public PeriodOfActivityInput PeriodOfActivity { get; }

        public TotalRailwayLengthInput TotalLength { get; }

        public RailwayGaugeInput Gauge { get; }

        public string? WebsiteUrl { get; }

        public string? Headquarters { get; }
    }
}
