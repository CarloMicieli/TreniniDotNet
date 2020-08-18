using TreniniDotNet.Application.Catalog.Railways.CreateRailway;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public static class NewCreateRailwayInput
    {
        public static readonly CreateRailwayInput Empty = With();

        public static CreateRailwayInput With(
            string name = null,
            string companyName = null,
            string country = null,
            PeriodOfActivityInput periodOfActivity = null,
            TotalRailwayLengthInput totalLength = null,
            RailwayGaugeInput gauge = null,
            string website = null,
            string headquarters = null) => new CreateRailwayInput(
            name, companyName, country, periodOfActivity, totalLength, gauge, website, headquarters);
    }
}
