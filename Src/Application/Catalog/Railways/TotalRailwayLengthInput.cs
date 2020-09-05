using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public sealed class TotalRailwayLengthInput
    {
        public TotalRailwayLengthInput(decimal? kilometers, decimal? miles)
        {
            Kilometers = kilometers;
            Miles = miles;
        }

        public decimal? Kilometers { get; }
        public decimal? Miles { get; }

        public void Deconstruct(out decimal? km, out decimal? mi)
        {
            km = Kilometers;
            mi = Miles;
        }

        public RailwayLength? ToRailwayLength()
        {
            var (km, mi) = this;
            return RailwayLength.TryCreate(km, mi, out var rl) ? rl : null;
        }

        public static TotalRailwayLengthInput Default() =>
            new TotalRailwayLengthInput(null, null);
    }
}
