namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class Prototype
    {
        public Prototype(string? className, string? roadNumber, string? typeName, string? series)
        {
            ClassName = className;
            RoadNumber = roadNumber;
            TypeName = typeName;
            Series = series;
        }

        public string? ClassName { get; }

        public string? RoadNumber { get; }

        public string? TypeName { get; }

        public string? Series { get; }

        public static Prototype OfLocomotive(string className, string roadNumber) =>
            new Prototype(className, roadNumber, null, null);

        public static Prototype OfFreightCar(string typeName) =>
            new Prototype(null, null, typeName, null);

        public static Prototype OfPassengerCar(string typeName) =>
            new Prototype(null, null, typeName, null);
    }
}
