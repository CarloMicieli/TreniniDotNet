namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class Prototype
    {
        public Prototype(string? className, string? roadNumber, string? typeName)
        {
            ClassName = className;
            RoadNumber = roadNumber;
            TypeName = typeName;
        }

        public string? ClassName { get; }

        public string? RoadNumber { get; }

        public string? TypeName { get; }

        public static Prototype OfLocomotive(string className, string roadNumber) =>
            new Prototype(className, roadNumber, null);

        public static Prototype OfFreightCar(string typeName) =>
            new Prototype(null, null, typeName);

        public static Prototype OfPassengerCar(string typeName) =>
            new Prototype(null, null, typeName);
    }
}
