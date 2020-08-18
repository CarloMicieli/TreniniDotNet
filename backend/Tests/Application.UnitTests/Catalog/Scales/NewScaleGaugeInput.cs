namespace TreniniDotNet.Application.Catalog.Scales
{
    public static class NewScaleGaugeInput
    {
        public static ScaleGaugeInput With(
            string trackGauge = null,
            decimal? inches = null,
            decimal? millimeters = null) =>
            new ScaleGaugeInput(trackGauge, inches, millimeters);
    }
}
