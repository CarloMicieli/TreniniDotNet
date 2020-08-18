using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public static class NewEditRailwayInput
    {
        public static readonly EditRailwayInput Empty = With();

        public static EditRailwayInput With(
            Slug? railwaySlug = null,
            string name = null,
            string companyName = null,
            string country = null,
            PeriodOfActivityInput periodOfActivity = null,
            TotalRailwayLengthInput totalLength = null,
            RailwayGaugeInput railwayGauge = null,
            string website = null,
            string headquarters = null) => new EditRailwayInput(
                railwaySlug ?? Slug.Empty,
                name, companyName, country,
                periodOfActivity, totalLength, railwayGauge, website, headquarters);
    }
}
