namespace TreniniDotNet.Application.Catalog.Railways
{
    public static class NewRailwayGaugeInput
    {
        public static RailwayGaugeInput With(
            string trackGauge = null,
            decimal? millimeters = null,
            decimal? inches = null) =>
            new RailwayGaugeInput(trackGauge, millimeters, inches);
    }
}
