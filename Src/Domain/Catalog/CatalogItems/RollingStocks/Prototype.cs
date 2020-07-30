namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class Prototype
    {
        private Prototype() { }

        private Prototype(string className, string roadNumber, string? series)
        {
            ClassName = className;
            RoadNumber = roadNumber;
            Series = series;
        }

        public string ClassName { get; } = null!;

        public string RoadNumber { get; } = null!;

        public string? Series { get; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Series))
            {
                return $"Prototype({ClassName}, {RoadNumber})";
            }
            else
            {
                return $"Prototype({ClassName}, {RoadNumber}, {Series})";
            }
        }

        public static Prototype OfLocomotive(string className, string roadNumber) =>
            new Prototype(className, roadNumber, null);

        public static Prototype OfLocomotive(string className, string roadNumber, string? series) =>
            new Prototype(className, roadNumber, series);
    }
}
