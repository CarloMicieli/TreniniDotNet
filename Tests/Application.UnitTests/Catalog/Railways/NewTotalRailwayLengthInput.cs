namespace TreniniDotNet.Application.Catalog.Railways
{
    public static class NewTotalRailwayLengthInput
    {
        public static TotalRailwayLengthInput With(
            decimal? kilometers = null,
            decimal? miles = null) =>
            new TotalRailwayLengthInput(kilometers, miles);
    }
}
