using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
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
}
